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
            List<Qubit> pretendReceiver = new List<Qubit>();
            //
            Random random = new Random();
            StringBuilder sb;

            //message commands are written to debug so the message is hidden from end user atm (mostly having fun with this)
            Debug.WriteLine("- Generating message.....");
            byte[] msg = new byte[msgLength];
            random.NextBytes(msg);
            sb = new StringBuilder();
            foreach (byte b in msg)
            {
                sb.Append(b);
            }
            string msgString = sb.ToString();
            Debug.WriteLine("- Generated!\n- The message to be encrypted is " + msgString);

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


            Console.WriteLine("- [ALL] Comparing polarization types and forming key...");

            StringBuilder secretKey = new StringBuilder();
            for (int i = 0; i < receiverPol.Count; i++)
            {
                if (receiverPol[i] == transmitPol[i])
                {
                    secretKey.Append(pretendTransmission[i].measure(transmitPol[i]));
                }
            }

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
