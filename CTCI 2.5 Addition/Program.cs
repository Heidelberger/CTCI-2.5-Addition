using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTCI_2._5_Addition
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeaderMsg(2, 3, "Addition via Nodes");
            
            Node head1 = CreateSinglyLinkedList(10);
            Node head2 = CreateSinglyLinkedList(10);

            PrintNodes(head1);
            Console.WriteLine("+");
            PrintNodes(head2);
            Console.WriteLine("--------------");

            AddLists(head1, head2);

            Console.ReadLine();
        }

        private static void AddLists(Node head1, Node head2)
        {
            // numbers are stored reverse-order (tail is 1s digit)
            // each node is a single digit

            string str_num1 = "";
            Node runner = head1;
            while (runner != null)
            {
                str_num1 += runner.Data;
                runner = runner.next;
            }
            char[] charArray = str_num1.ToArray();
            Array.Reverse(charArray);
            str_num1 = new string(charArray);
            
            string str_num2 = "";
            runner = head2;
            while (runner != null)
            {
                str_num2 += runner.Data;
                runner = runner.next;
            }
            charArray = str_num2.ToArray();
            Array.Reverse(charArray);
            str_num2 = new string(charArray);

            ulong num1 = ulong.Parse(str_num1);
            ulong num2 = ulong.Parse(str_num2);

            ulong num3 = num1 + num2;

            Console.WriteLine(num1);
            Console.WriteLine("+");
            Console.WriteLine(num2);
            Console.WriteLine("------------------");
            Console.WriteLine(num3);
            
        }

        private static Node CreateSinglyLinkedList(int count)
        {
            if (count < 1)
                return null;

            Random rnd = new Random((int)DateTime.Now.Ticks);

            Node head = new Node(rnd.Next(0, 9));

            Node n = head;

            for (int i = 0; i < count - 1; ++i)
            {
                n.next = new Node(rnd.Next(0, 9));
                n = n.next;
            }

            return head;
        }

        private static void PrintNodes(Node passed_n)
        {
            Console.WriteLine("Nodes in list:");

            while (passed_n != null)
            {
                Console.Write(passed_n.Data + ", ");

                passed_n = passed_n.next;
            }
            Console.WriteLine();
        }

        private static void PrintHeaderMsg(int chapter, int problem, string title)
        {
            Console.WriteLine("Cracking the Coding Interview");
            Console.WriteLine("Chapter " + chapter + ", Problem " + chapter + "." + problem + ": " + title);
            Console.WriteLine();
        }
    }

    class Node
    {
        public Node next = null;

        public int Data;

        public Node(int d) => Data = d;
    }
}
