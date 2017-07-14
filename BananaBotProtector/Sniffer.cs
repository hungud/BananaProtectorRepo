using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Linq;

namespace BananaBotProtector
{
    public class Sniffer
    {
        private struct ArrivedPacket
        {
            public int number;
            public TimeSpan Capturetime;
            public IPv4Packet packet;
            public ArrivedPacket(int number, TimeSpan CaptureTime, IPv4Packet packet)
            {
                this.number = number;
                this.Capturetime = CaptureTime;
                this.packet = packet;
            }
        }
        private class Counter : Dictionary<string, int>
        {
            public void AddStat(IPAddress Address)
            {
                string key = Address.ToString();
                if (this.ContainsKey(key))
                {
                    this[key] += 1;
                }
                else
                {
                    //RemoveMin();
                    Add(key, 0);
                }
            }
            public List<KeyValuePair<string, int>> getCurShot()
            {
                RemoveMin();
                return (from entry in this orderby entry.Value descending select entry).ToList();  //do we need to lock?
            }
            private void RemoveMin()
            {
                List<string> toRemove = new List<string>();
                foreach (KeyValuePair<string, int> pair in this)
                {
                    if (pair.Value == 0)
                    {
                        toRemove.Add(pair.Key);
                    }
                }

                foreach (var key in toRemove)
                {
                    this.Remove(key);
                }
            }
            public void Resetall()
            {
                string[] keys = Keys.ToArray();
                for (int i = 0; i < keys.Length; ++i)
                {
                    this[keys[i]] = 0;
                }
            }
        }
        private Counter MyCounter;
        private volatile List<ArrivedPacket> PacketThread;
        // volatile IAsyncResult currentAsyncResult;
        private Object Locker = new Object();
        private Socket Listener;
        private volatile Stopwatch Sw;
        private NetworkInterface _adapter;
        //private StateOb Obj;
        public NetworkInterface Adapter
        {
            get
            {
                return _adapter;
            }
            private set
            {
                _adapter = value;
                MyAddress = NetworkInstruments.getAdapterIPAddress(_adapter);
            }
        }
        public int ReceivedAmount { get; private set; }
        public int DroppedAmount { get; private set; }
        public bool State;
        //[] newdata;
        private volatile List<string> BriefPackets;
        private volatile IPAddress MyAddress;
        private void InitSocket()
        {
            Listener = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
            Listener.Bind(new IPEndPoint(NetworkInstruments.getAdapterIPAddress(Adapter), 0));
            Listener.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);
            Listener.IOControl(IOControlCode.ReceiveAll, new byte[4] { 1, 0, 0, 0 }, new byte[4] { 1, 0, 0, 0 });
            Listener.ReceiveBufferSize = 2147483647; // 2^31
            Listener.SendBufferSize = 2147483647;
            Listener.SetIPProtectionLevel(IPProtectionLevel.Unrestricted);
        }
        private class StateOb
        {
            public byte[] buffer;
        }
        private void OnReceive(IAsyncResult res)
        {


            try
            {
                StateOb ob = (StateOb)res.AsyncState;
                int n = Listener.EndReceive(res);
                byte[] data = ob.buffer;
                ob.buffer = new byte[4096];
                Listener.BeginReceive(ob.buffer, 0, ob.buffer.Length, SocketFlags.None, OnReceive, ob);
                processPacket(data, n);
            }
            catch (OutOfMemoryException ex)
            {
                Error(ex);
                StopCapture();
            }
            catch (Exception)
            {

                return;
            }



        }
        private void processPacket(byte[] data, int len)
        {
            byte[] pac = new byte[len];
            Array.Copy(data, pac, len);
            ArrivedPacket packet = new ArrivedPacket(ReceivedAmount, Sw.Elapsed, new IPv4Packet(pac));
            lock (Locker)
            {
                PacketThread.Add(packet);
                string curType = Enum.GetName(typeof(MyProtocolType), ((packet.packet.protocolType == MyProtocolType.Tcp) ? (((TcpPacket)(packet.packet.ChildPacket)).HttpInside) ? MyProtocolType.Http : MyProtocolType.Tcp : packet.packet.protocolType));
                BriefPackets.Add(packet.number.ToString() + "+" + packet.Capturetime.ToString("mm\\:s\\.fffffff") + "+" + packet.packet.SourceAddress.ToString() + "+" + packet.packet.DestAddress.ToString() + "+" + packet.packet.TTL.ToString() + "+" + curType + "+" + packet.packet.TotalLength.ToString() + "+" + packet.packet.Description());
                if (!packet.packet.SourceAddress.Equals(MyAddress) && curType == "Http")
                {
                    MyCounter.AddStat(packet.packet.SourceAddress);
                }
                ReceivedAmount++;
            }
        }


        public string[] getData()
        {
            lock (Locker)
            {
                string[] packets = new string[BriefPackets.Count];
                BriefPackets.CopyTo(packets, 0);
                BriefPackets.Clear();
                return packets;
            }
        }
        public List<KeyValuePair<string, int>> getStatInfo()
        {
            List<KeyValuePair<string, int>> res;
            lock (Locker)
            {
                res = MyCounter.getCurShot();
                MyCounter.Resetall(); 
            }
            return res;
        }

        public delegate void ExceptionHandler(System.Exception ex);
        public ExceptionHandler Error;
        public Sniffer(NetworkInterface Adapter, ExceptionHandler Handler)
        {
            this.Adapter = Adapter;
            PacketThread = new List<ArrivedPacket>();
            Sw = new Stopwatch();
            BriefPackets = new List<string>();
            Error = Handler;
            MyCounter = new Counter();
        }
        public void StartCapture()
        {
            ResetAll();
            State = true;
            InitSocket();
            Sw.Start();
            StateOb Obj = new StateOb();
            Obj.buffer = new byte[4096];
            Listener.BeginReceive(Obj.buffer, 0, Obj.buffer.Length, SocketFlags.None, OnReceive, Obj);
        }
        public void StopCapture()
        {
            if (State)
            {
                State = false;
                Listener.Close();
                Sw.Stop();
            }
        }
        public IPv4Packet getPacket(int ind)
        {
            try
            {
                return PacketThread[ind].packet;

            }
            catch (Exception)
            {

                throw new Exception(); //wrong packet number exception
            }
        }
        public void ResetAll()
        {
            if (State)
            { StopCapture(); }
            Sw.Reset();
            PacketThread.Clear();
            BriefPackets.Clear();
            ReceivedAmount = 0;
            DroppedAmount = 0;
        }

        ~Sniffer()
        {
            if (State)
            {
                StopCapture();
                ResetAll();
            }
        }
    }
}


