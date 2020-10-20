using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace COMPX304_Qubit
{
    class Program
    {
        static void Main(string[] args)
        {
            //keyLength is a misnomer here, it's actually the number of qubits exchanged between transmitter and receiver
            int keyLength = 256;
            int msgLength = 20;
            // once i'm sure on the enc/dec stuff i will write some basic networking support for transmit/recv but for now the transmitter will store their qubits in here and the receiver will store theirs in another list.
            List<Qubit> pretendTransmission = new List<Qubit>();
            
            //
            Random random = new Random();
            StringBuilder sb;

            //message commands are written to debug so the message is hidden from end user atm (mostly having fun with this)
            Console.WriteLine("- [DEBUG] Generating message.....");
            byte[] msg = new byte[msgLength];
            random.NextBytes(msg);
            sb = new StringBuilder();
            foreach (byte b in msg)
            {
                sb.Append(b);
            }
            string msgString = sb.ToString();
            Console.WriteLine("- [DEBUG] Generated!\n- [DEBUG] The message to be encrypted is " + msgString);

            //now comes the fun times
            Console.WriteLine("- [TRANSMITTER] Generating qubits...");
            List<int> transmitPol = new List<int>();
            for (int i = 0; i < keyLength; i++)
            {
                int pol = 0;
                int val = 0;

                int cOrL = random.Next(100);
                if (cOrL > 50)
                {
                    pol = 1;
                }

                int valRan = random.Next(100);
                if (valRan > 50)
                {
                    val = 1;
                }

                Qubit newQubit = new Qubit(val, pol);
                pretendTransmission.Add(newQubit);
                transmitPol.Add(pol);
            }

            Console.WriteLine("- [RECEIVER] Measuring qubits...");
            List<int> receiverVal = new List<int>();
            List<int> receiverPol = new List<int>();

            foreach (Qubit q in pretendTransmission)
            {
                int pol = 0;
                int cOrL = random.Next(100);
                if (cOrL > 50)
                {
                    pol = 1;
                }

                int val = q.measure(pol);
                receiverVal.Add(val);
                receiverPol.Add(pol);
            }

            Console.WriteLine("- [FAKE NETWORKING, NO PACKETS SENT] [TRANSMITTER] Exchanging polarizations...");

            // Because I haven't written any actual message sending, this sorta demonstrates how the MITM attack would actually work.
            //Pretend that here, the packets have been sniffed between machines
            StringBuilder pol1 = new StringBuilder();
            foreach (int i in transmitPol)
            {
                pol1.Append(i);
            }
            Console.WriteLine("- [FAKE NETWORKING, NO PACKETS SENT] [MITM] 1st set of polarizations are: " + pol1.ToString());



            Console.WriteLine("- [FAKE NETWORKING, NO PACKETS SENT] [RECEIVER] Exchanging polarizations...");
            StringBuilder pol2 = new StringBuilder();
            foreach (int i in receiverPol)
            {
                pol2.Append(i);
            }
            Console.WriteLine("- [FAKE NETWORKING, NO PACKETS SENT] [MITM] 2nd set of polarizations are: " + pol2.ToString());


            Console.WriteLine("- [TRNSMT/RECV] Comparing polarization types and forming key...");

            StringBuilder secretKey = new StringBuilder();
            for (int i = 0; i < receiverPol.Count; i++)
            {
                if (receiverPol[i] == transmitPol[i])
                {
                    secretKey.Append(pretendTransmission[i].measure(transmitPol[i]));
                }
            }

            Console.WriteLine("- [MITM] In a real scenario, a man-in-the-middle attack would sniff the packets going each way with the polarizations as well as the Qubits themselves, then do the exact same comparison.\n- [MITM] Because there's no actual packets being sent, the key being printed twice is a waste of time.");

            Console.WriteLine("- [ALL] The secret key is: " + secretKey.ToString());

            Console.WriteLine("- [TRANSMITTER] Encrypting message...");
            string encMsg = XOR.Encrypt(msgString, secretKey.ToString());
            Console.WriteLine("- [TRANSMITTER] Message encrypted!");

            Console.WriteLine("- [RECEIVER] Decrypting message...");
            string decMsg = XOR.Decrypt(encMsg, secretKey.ToString());
            Console.WriteLine("- [RECEIVER] Message decrypted! Checking if decryption succeeded...");
            if (decMsg.Equals(msgString))
            {
                Console.WriteLine("- [DEBUG] MESSAGE ENCRYPTED AND DECRYPTED SUCCESSFULLY!!");
            }
            else
            {
                Console.WriteLine("- [DEBUG] MESSAGE FAILED TO ENCRYPT AND DECRYPT!!!");
            }
        }
    }
}
