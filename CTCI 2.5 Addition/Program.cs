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

            Node head1 = CreateSinglyLinkedList(5);
            Node head2 = CreateSinglyLinkedList(10);
            Node answer = null;
            EvenListLength(head1, head2);
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

        /// <summary>
        /// 
        /// This method adds leading zeros to the shorter list
        /// to give both lists the same number of nodes
        /// 
        /// </summary>
        /// <param name="head1"></param>
        /// <param name="head2"></param>
        private static void EvenListLength(Node head1, Node head2)
        {

            int length1 = GetListLength(head1);
            int length2 = GetListLength(head2);

            if (length1 > length2)
            {
                for (int i = 0; i < (length1 - length2); ++i)
                {
                    AddTrailingZero(head2);
                }
            }

            if (length2 > length1)
            {
                for (int i = 0; i < (length2 - length1); ++i)
                {
                    AddTrailingZero(head1);
                }
            }
        }


        /// <summary>
        /// 
        /// 1. adds the passed numbers and passed remainder
        /// 2. records the answer as a new node in the answer list
        /// 2. calls itself, passing the next 2 numbers in the list, and the new remainder
        /// 3. repeats 1-3 until both nodes are null (end of list)
        /// 4. if there's a remainder, it's added to the answer list
        /// 
        /// Complexity:     Runs in O(N) time
        ///                 Every node is touched once
        ///                 
        ///                 Requires O(N) memory
        ///                 The answer is built in a new linked list, which
        ///                 is approximately as large as the largest input list.
        ///                 As input grows, memory requirement grows.
        /// 
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <param name="answer"></param>
        /// <param name="remainder"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Utility function to add zeroes at beginning of list
        /// </summary>
        /// <param name="node1"></param>
        private static void AddLeadingZero(ref Node node1)
        {
            Node temp = new Node(0);
            temp.next = node1;
            node1 = temp;
        }
        
        /// <summary>
        /// Utility function to add zeroes at end of list        
        /// </summary>
        /// <param name="head1"></param>
        private static void AddTrailingZero(Node head1)
        {
            Node runner = head1;

            while (runner.next != null)
                runner = runner.next;

            runner.next = new Node(0);

        }

        private static int GetListLength(Node node1)
        {
            if (node1 == null)
                return 0;

            int counter = 1;

            while (node1.next != null)
            {
                ++counter;
                node1 = node1.next;
            }

            return counter;
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
