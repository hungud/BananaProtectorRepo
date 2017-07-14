using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;

namespace BananaBotProtector
{
    public partial class Form1 : Form
    {
        private Timer UpdateTimer;
        private delegate void SafePacketAdd(string packet);
        private volatile bool ScrollToLast;
        private Sniffer MySniffer;
        private string[] ListViewdata;
        public Form1()
        {
            InitializeComponent();
            System.Reflection.PropertyInfo controlProperty = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            controlProperty.SetValue(PacketStreamView, true, null);
            MySniffer = new Sniffer(NetworkInstruments.getAnyAdaptor(), ErrorHandler);
            AdapterLab.Text = MySniffer.Adapter.Name;
            ScrollToLast = true;
            UpdateTimer = new Timer();
            UpdateTimer.Tick += UpdateData;
            UpdateTimer.Interval = 500;
        }

        private void UpdateData(object sender, EventArgs e)
        {
            ListViewdata = MySniffer.getData();
            List<KeyValuePair<string, int>> Counter = MySniffer.getStatInfo();
            try
            {
                if (ListViewdata.Length != 0)
                {
                    Color Back = Color.LightGray;
                    Color Fore = Color.Linen;
                    ListViewItem[] Items = new ListViewItem[ListViewdata.Length];
                    for (int i = 0; i < Items.Length; ++i)
                    {
                        Items[i] = new ListViewItem(ListViewdata[i].Split('+'));
                        switch (Items[i].SubItems[5].Text)
                        {
                            case "Icmp":
                                Back = Color.Blue;
                                Fore = Color.Yellow;
                                break;
                            case "Tcp":
                                Back = Color.FromArgb(18, 39, 46);
                                Fore = Color.FromArgb(247, 135, 135);
                                break;
                            case "Udp":
                                Back = Color.FromArgb(254, 255, 208);
                                Fore = Color.Black;
                                break;
                            case "Http":
                                Back = Color.Black;
                                Fore = Color.Yellow;
                                break;
                            default:
                                Back = Color.LightGray;
                                Fore = Color.Linen;
                                break;
                        }
                        Items[i].BackColor = Back;
                        Items[i].ForeColor = Fore;
                    }
                    PacketStreamView.Items.AddRange(Items);
                    if (ScrollToLast)
                    {
                        PacketStreamView.EnsureVisible(PacketStreamView.Items.Count - 1);
                    }
                    CounterGrid.RowCount = Counter.Count;
                    for (int i = 0; i < Counter.Count; ++i)
                    {
                        CounterGrid.Rows[i].Cells[0].Value = Counter[i].Key;
                        CounterGrid.Rows[i].Cells[1].Value = Counter[i].Value.ToString();
                    }
                    TotalSentLab.Text = MySniffer.ReceivedAmount.ToString();
                    //DropppedLab.Text = MySniffer.DroppedAmount.ToString();
                }
            }
            catch (OutOfMemoryException)
            {
                Stop(); /// add error explanation
                MessageBox.Show("Из за ограничения памяти достигнут лимит захвата пакетов, захват можно продолжить перезапустив сниффер","Достигнут лимит захвата пакетов",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            //TotalSentLab.Text = MySniffer.ReceivedAmount.ToString();


        }

        private void ChangeAdapterBtn_Click(object sender, EventArgs e)
        {
            AdapterChoosingForm sets = new AdapterChoosingForm(MySniffer.Adapter, SetAdapter);
            sets.Show();
        }
        private void SetAdapter(NetworkInterface NewAdapter)
        {
            MySniffer = null;
            PacketStreamView.Items.Clear();
            MySniffer = new Sniffer(NewAdapter, ErrorHandler);
            AdapterLab.Text = NewAdapter.Name;
        }
        private void ErrorHandler(Exception exc)
        {
            Stop();
            MessageBox.Show("Из за ограничения памяти достигнут лимит захвата пакетов, захват можно продолжить перезапустив сниффер", "Достигнут лимит захвата пакетов", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private string getArrayBits(byte[] ar)
        {
            BitArray t = new BitArray(ar.Reverse().ToArray());
            string str = "";
            int i = 0;
            int j;
            while (i < t.Length)
            {
                j = 0;
                while (j < 8)
                {
                    str += (t[i]) ? "1" : "0";
                    ++i;
                    ++j;
                }
                str += " ";
            }
            return str;
        }
        private string getString(BitArray arr)
        {
            string res = "";
            for (int i = 0; i < arr.Length; ++i)
            {
                res += (arr[i]) ? "1" : "0";
            }
            return res;
        }
        private void EnableBtn_Click(object sender, EventArgs e)
        {
            PacketStreamView.Items.Clear();
            MySniffer.StartCapture();
            UpdateTimer.Start();
        }
        private void DisableBtn_Click(object sender, EventArgs e)
        {

            Stop();
        }
        private void Stop()
        {
            UpdateTimer.Stop();
            MySniffer.StopCapture();
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            UpdateTimer.Stop();
            MySniffer.StopCapture();
            MySniffer.ResetAll();
            PacketStreamView.Items.Clear();

        }

        private void Scroll_CheckedChanged(object sender, EventArgs e)
        {
            ScrollToLast = Scroller.Checked;
        }

        private void SetTimerBtn_Click(object sender, EventArgs e)
        {
            UpdateTimer.Interval = Convert.ToInt32(TimerNum.Value) * 1000;
        }


        private TreeNode parsePacket(Packet packet)
        {
            TreeNode CurNode = new TreeNode();
            switch (packet.PacketType)
            {
                case MyProtocolType.IP:
                    {
                        IPv4Packet Ippac = (IPv4Packet)packet;
                        CurNode.Text = "Internet Protocol v4 -  Отправитель: " + Ippac.SourceAddress.ToString() + " Получатель: " + Ippac.DestAddress.ToString();
                        CurNode.Nodes.Add("Длина заголовка: " + Ippac.HeaderLength.ToString() + " (" + Ippac.HeaderLength * 4 + ") байт");
                        CurNode.Nodes.Add("DSCP:" + Ippac.DSCP.ToString());
                        CurNode.Nodes.Add("Общая длина: " + Ippac.TotalLength.ToString() + " байт");
                        CurNode.Nodes.Add("Флаги: " + getString(Ippac.Flags));
                        CurNode.Nodes.Add("Смещение фрагмента: " + getString(Ippac.FragmentOffset));
                        CurNode.Nodes.Add("TTL: " + Ippac.TTL.ToString());
                        CurNode.Nodes.Add("Протокол внутри: " + Enum.GetName(typeof(MyProtocolType), Ippac.protocolType) + " (" + Ippac.HeaderBytes[9] + ")");
                        CurNode.Nodes.Add("Чексумма заголовка: " + Ippac.HeaderChecksum.ToString());
                        CurNode.Nodes.Add("Отправитель: " + Ippac.SourceAddress.ToString());
                        CurNode.Nodes.Add("Получатель: " + Ippac.DestAddress.ToString());
                        if (Ippac.HeaderLength > 5)
                        {
                            TreeNode Opt = new TreeNode("Опции");
                            Opt.Nodes.Add(getArrayBits(Ippac.Options));
                        }
                        TreeNode RawHeader = new TreeNode("Байты заголовка");
                        TreeNode RawData = new TreeNode("Байты нагрузки");
                        RawHeader.Nodes.Add(getArrayBits(Ippac.HeaderBytes));
                        RawData.Nodes.Add(getArrayBits(Ippac.ChildBytes));
                        CurNode.Nodes.Add(RawHeader);
                        CurNode.Nodes.Add(RawData);
                        CurNode.Nodes.Add(parsePacket(Ippac.ChildPacket));
                    }

                    break;
                case MyProtocolType.Icmp:
                    {
                        IcmpPacket Icmppac = (IcmpPacket)packet;
                        IcmpPacket.Type type = Icmppac.type;
                        byte Code = Icmppac.Code;
                        CurNode.Text = "Internet Control Message Protocol " + Icmppac.Description();
                        CurNode.Nodes.Add("Тип: " + Enum.GetName(typeof(IcmpPacket.Type), type));
                        CurNode.Nodes.Add("Код: " + Code);
                        CurNode.Nodes.Add("Чексумма: " + Icmppac.Checksum.ToString());
                        switch (type)
                        {
                            case IcmpPacket.Type.EchoReply:
                                {
                                    CurNode.Nodes.Add("Идентификатор: " + Icmppac.Identifier.ToString());
                                    CurNode.Nodes.Add("SequenceNumber: " + Icmppac.SequenceNumber.ToString());
                                }
                                break;
                            case IcmpPacket.Type.DestinationUnreachable:
                                {
                                    if (Code == 4) CurNode.Nodes.Add("NextHopMTU: " + Icmppac.NextHopMTU);
                                    CurNode.Nodes.Add(parsePacket(Icmppac.ChildPacket));
                                }
                                break;
                            case IcmpPacket.Type.RedirectMessage:
                                {
                                    CurNode.Nodes.Add("IP адрес: " + Icmppac.IpAddress.ToString());
                                    CurNode.Nodes.Add(parsePacket(Icmppac.ChildPacket));
                                }
                                break;
                            case IcmpPacket.Type.EchoRequest:
                                {
                                    CurNode.Nodes.Add("Идентификатор: " + Icmppac.Identifier.ToString());
                                    CurNode.Nodes.Add("SequenceNumber: " + Icmppac.SequenceNumber.ToString());
                                }
                                break;
                            case IcmpPacket.Type.RouterAdvertisment:
                                {
                                    CurNode.Nodes.Add("AdvertismentCount: " + Icmppac.AdvertismentCount.ToString());
                                    CurNode.Nodes.Add("AddressEntrySize: " + Icmppac.AddressEntrySize.ToString());
                                    CurNode.Nodes.Add("Время жизни: " + Icmppac.Lifetime.ToString());
                                }
                                break;
                            case IcmpPacket.Type.TimeExceeded:
                                {
                                    CurNode.Nodes.Add(parsePacket(Icmppac.ChildPacket));
                                }
                                break;
                            case IcmpPacket.Type.BadIpHeader:
                                {
                                    CurNode.Nodes.Add("Указатель: " + Icmppac.Pointer.ToString());
                                    CurNode.Nodes.Add(parsePacket(Icmppac.ChildPacket));

                                }
                                break;
                            case IcmpPacket.Type.TimeStamp:
                                {
                                    CurNode.Nodes.Add("Идентификатор: " + Icmppac.Identifier.ToString());
                                    CurNode.Nodes.Add("SequenceNumber: " + Icmppac.SequenceNumber.ToString());
                                    CurNode.Nodes.Add("OriginateTimestamp: " + Icmppac.OriginateTimestamp.ToString());
                                    CurNode.Nodes.Add("ReceiveTimestamp: " + Icmppac.ReceiveTimestamp.ToString());
                                    CurNode.Nodes.Add("TransmitTimestamp: " + Icmppac.TransmitTimestamp.ToString());

                                }
                                break;
                            case IcmpPacket.Type.TimeStampReply:
                                {
                                    CurNode.Nodes.Add("Идентификатор: " + Icmppac.Identifier.ToString());
                                    CurNode.Nodes.Add("SequenceNumber: " + Icmppac.SequenceNumber.ToString());
                                    CurNode.Nodes.Add("OriginateTimestamp: " + Icmppac.OriginateTimestamp.ToString());
                                    CurNode.Nodes.Add("ReceiveTimestamp: " + Icmppac.ReceiveTimestamp.ToString());
                                    CurNode.Nodes.Add("TransmitTimestamp: " + Icmppac.TransmitTimestamp.ToString());
                                }
                                break;
                        }
                        TreeNode RawHeader = new TreeNode("Байты заголовка");
                        TreeNode RawData = new TreeNode("Байты нагрузки");
                        RawHeader.Nodes.Add(getArrayBits(Icmppac.HeaderBytes));
                        RawData.Nodes.Add(getArrayBits(Icmppac.ChildBytes));
                        CurNode.Nodes.Add(RawHeader);
                        CurNode.Nodes.Add(RawData);
                    }
                    break;
                case MyProtocolType.Udp:
                    {
                        UdpPacket Udppac = (UdpPacket)packet;
                        CurNode.Text = "User Datagramm Protocol - Порт отправителя: " + Udppac.SourcePort + " Порт получателя: " + Udppac.DestPort;
                        CurNode.Nodes.Add("Порт отправителя: " + Udppac.SourcePort.ToString());
                        CurNode.Nodes.Add("Порт получателя: " + Udppac.DestPort.ToString());
                        CurNode.Nodes.Add("Чексумма: " + Udppac.Checksum.ToString());
                        CurNode.Nodes.Add("Длина: " + Udppac.Length.ToString() + " байт");
                        TreeNode RawHeader = new TreeNode("Байты заголовка");
                        TreeNode RawData = new TreeNode("Байты нагрузки");
                        RawHeader.Nodes.Add(getArrayBits(Udppac.HeaderBytes));
                        RawData.Nodes.Add(getArrayBits(Udppac.ChildBytes));
                        CurNode.Nodes.Add(RawHeader);
                        CurNode.Nodes.Add(RawData);
                    }

                    break;
                case MyProtocolType.Tcp:
                    {
                        TcpPacket Tcppac = (TcpPacket)packet;
                        CurNode.Text = "Transmission Control Protocol " + Tcppac.Description();
                        CurNode.Nodes.Add("Порт отправителя: " + Tcppac.SourcePort.ToString());
                        CurNode.Nodes.Add("Порт получателя: " + Tcppac.DestPort.ToString());
                        CurNode.Nodes.Add("Sequence Number: " + Tcppac.SequenceNumber.ToString());
                        CurNode.Nodes.Add("AcknoledgementNumber: " + Tcppac.AcknowledgementNumber.ToString());
                        CurNode.Nodes.Add("Длина заголовка: " + Tcppac.HeaderLength.ToString() + " ( " + Tcppac.HeaderLength * 4 + " байт)");
                        CurNode.Nodes.Add("Флаги: " + getString(Tcppac.Flags));
                        CurNode.Nodes.Add("Размер окна : " + Tcppac.WindowSize.ToString());
                        CurNode.Nodes.Add("Чексумма: " + Tcppac.CheckSum.ToString());
                        CurNode.Nodes.Add("Указатель важности: " + Tcppac.UrgentPointer.ToString());
                        if (Tcppac.HeaderLength > 5)
                        {
                            TreeNode Opt = new TreeNode("Опции");
                            Opt.Nodes.Add(getArrayBits(Tcppac.Options));
                            CurNode.Nodes.Add(Opt);
                        }
                        TreeNode RawHeader = new TreeNode("Байты заголовка");
                        TreeNode RawData = new TreeNode("Байты нагрузки");
                        RawHeader.Nodes.Add(getArrayBits(Tcppac.HeaderBytes));
                        RawData.Nodes.Add(getArrayBits(Tcppac.ChildBytes));
                        CurNode.Nodes.Add(RawHeader);
                        CurNode.Nodes.Add(RawData);

                    }
                    break;
                //case MyProtocolType.Http:
                //    {
                //        TcpPacket Tcppac = (TcpPacket)packet;
                //        CurNode.Text = "Transmission Control Protocol " + Tcppac.Description();
                //        CurNode.Nodes.Add("Порт отправителя: " + Tcppac.SourcePort.ToString());
                //        CurNode.Nodes.Add("Порт получателя: " + Tcppac.DestPort.ToString());
                //        CurNode.Nodes.Add("Sequence Number: " + Tcppac.SequenceNumber.ToString());
                //        CurNode.Nodes.Add("AcknoledgementNumber: " + Tcppac.AcknowledgementNumber.ToString());
                //        CurNode.Nodes.Add("Длина заголовка: " + Tcppac.HeaderLength.ToString() + " ( " + Tcppac.HeaderLength * 4 + " байт)");
                //        CurNode.Nodes.Add("Флаги: " + getString(Tcppac.Flags));
                //        CurNode.Nodes.Add("Размер окна : " + Tcppac.WindowSize.ToString());
                //        CurNode.Nodes.Add("Чексумма: " + Tcppac.CheckSum.ToString());
                //        CurNode.Nodes.Add("Указатель важности: " + Tcppac.UrgentPointer.ToString());
                //        if (Tcppac.HeaderLength > 5)
                //        {
                //            TreeNode Opt = new TreeNode("Опции");
                //            Opt.Nodes.Add(getArrayBits(Tcppac.Options));
                //        }
                //        TreeNode RawHeader = new TreeNode("Байты заголовка");
                //        TreeNode RawData = new TreeNode("Байты нагрузки");
                //        RawHeader.Nodes.Add(getArrayBits(Tcppac.HeaderBytes));
                //        RawData.Nodes.Add(getArrayBits(Tcppac.ChildBytes));
                //        CurNode.Nodes.Add(RawHeader);
                //        CurNode.Nodes.Add(RawData);
                //        if (Tcppac.HttpInside)
                //        {
                //            TreeNode Http = new TreeNode("Hypertext Transport Protocol");
                //            string text = Encoding.ASCII.GetString(Tcppac.ChildBytes);
                //            TreeNode HtContent = new TreeNode();
                //            Http.Nodes.Add(HtContent);
                //            CurNode.Nodes.Add(Http);
                //            text.Replace(System.Environment.NewLine, string.Empty);
                //            HtContent.Text = text;
                //        }
                //    }
                //    break;
                default: break;
            }
            return CurNode;
        }
        private void PacketStreamView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection ind = PacketStreamView.SelectedIndices;
            if (ind.Count > 0)
            {
                IPv4Packet CurPacket = MySniffer.getPacket(ind[0]);
                PacketInfoView.Nodes.Clear();
                PacketInfoView.Nodes.Add(parsePacket(CurPacket));
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


    }
}
