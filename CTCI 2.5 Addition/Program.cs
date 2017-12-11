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
            
            Node head1 = CreateSinglyLinkedList(100);
            Node head2 = CreateSinglyLinkedList(100);
            Node answer = null;


            PrintNodes(head1);
                Console.WriteLine("+");
            PrintNodes(head2);            
                Console.WriteLine();
                Console.WriteLine("Recursive Algorithm:");
                Console.WriteLine();
            PrintNodes_ReverseNum(head1);
                Console.WriteLine();
                Console.WriteLine("+");
            PrintNodes_ReverseNum(head2);
                Console.WriteLine();
                Console.WriteLine("--------------");       

            AddLists_LongHand_Recursive(head1, head2, ref answer);     
            PrintNodes_ReverseNum(answer);
                Console.WriteLine();

            Console.ReadLine();
        }


        private static int AddLists_LongHand_Recursive(Node node1, Node node2, ref Node answer, int remainder = 0)
        {
            // both lists exhausted, begin unspooling the stack
            if ((node1 == null) && (node2 == null))
            {
                if (remainder != 0)
                    AddNode(ref answer, 1);

                return 0;
            }

            int this1 = 0;
            int this2 = 0;            

            // if either node is null, use zero
            // this allows for unequal list addition
            // list is stored reverse-order, so zeroes
            // at the end are really zeroes at the 
            // beginning

            if (node1 != null)
            {
                this1 = node1.Data;
                if (node1.next != null)
                {
                    node1.Data = node1.next.Data;
                    node1.next = node1.next.next;
                }
                else
                    node1 = null; // end of list
            }

            if (node2 != null)
            {
                this2 = node2.Data;
                if (node2.next != null)
                {
                    node2.Data = node2.next.Data;
                    node2.next = node2.next.next;
                }
                else
                    node2 = null; // end of list
            }

            // because the list is reversed, we work head-to-tail
            // (head is the 1s place)
            
            AddNode(ref answer, (this1 + this2 + remainder) >= 10 ? (this1 + this2 + remainder) - 10 : (this1 + this2 + remainder));

            remainder = (this1 + this2 + remainder) / 10;

            remainder = AddLists_LongHand_Recursive(node1, node2, ref answer, remainder);

            return remainder;
        }


        private static void AddLists_LongHand(Node head1, Node head2)
        {
            Node runner1 = head1;
            Node runner2 = head2;
            Node answer = null;

            int remainder = 0;

            while ((runner1 != null) && (runner2 != null))
            {
                // get the lowest digit from both lists
                // NOTE: numbers are reverse-sorted (1s are last)

                while ((runner1.next != null) && (runner2.next != null))
                {
                    runner1 = runner1.next;
                    runner2 = runner2.next;
                }

                remainder = (runner1.Data + runner2.Data) % 10;

                AddNode(ref answer, (runner1.Data + runner2.Data - (remainder * 10))  );

                

                runner1 = head1;
                runner2 = head2;
            }






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

        private static void PrintNodes_ReverseNum(Node passed_node)
        {
            if (passed_node.next != null)
                PrintNodes_ReverseNum(passed_node.next);

            Console.Write(passed_node.Data);            
        }


        private static void AddNode(ref Node passed_head, int value)
        {
            if (passed_head == null)
                passed_head = new Node(value);
            else
            {
                passed_head.ApppendToTail(value);
            }
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

        public void ApppendToTail(int d)
        {
            Node n = this;

            while (n.next != null)
            {
                n = n.next;
            }

            n.next = new Node(d);
        }
    }
}
