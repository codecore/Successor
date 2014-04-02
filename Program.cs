using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DonCode
{
    public class Node<T> {
        public override string ToString() { return this.Value.ToString(); }
        public Node(T value) { this.Value = value; }
        public T Value { get; set; }
        private Node<T> left = null;
        public Node<T> Left { get { return this.left; } set { this.left = value; } }
        private Node<T> right = null;
        public Node<T> Right { get { return this.right; } set { this.right = value; } }
        private Node<T> parent = null;
        public Node<T> Parent { get { return this.parent; } set { this.parent = value; } }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            Node<int> root = new Node<int>(70);
            Add<int>(root,new Node<int>(0),compare_int);
            Add<int>(root,new Node<int>(-20),compare_int);
            Add<int>(root,new Node<int>(-30),compare_int);
            Add<int>(root,new Node<int>(-10),compare_int);
            Add<int>(root,new Node<int>(50),compare_int);
            Add<int>(root,new Node<int>(20),compare_int);
            Add<int>(root,new Node<int>(10),compare_int);
            Add<int>(root,new Node<int>(5),compare_int);
            Add<int>(root,new Node<int>(30),compare_int);
            Add<int>(root,new Node<int>(35),compare_int);
            Add<int>(root,new Node<int>(15),compare_int);
            Add<int>(root,new Node<int>(12),compare_int);
            Add<int>(root,new Node<int>(75),compare_int);
            Add<int>(root,new Node<int>(120),compare_int);
            Add<int>(root,new Node<int>(110),compare_int);
            Add<int>(root,new Node<int>(112),compare_int);
            Add<int>(root,new Node<int>(113),compare_int);
            Add<int>(root,new Node<int>(114),compare_int);
            Add<int>(root,new Node<int>(100),compare_int);
            Add<int>(root,new Node<int>(106),compare_int);
            Add<int>(root,new Node<int>(108),compare_int);
            Add<int>(root,new Node<int>(107),compare_int);
            Add<int>(root,new Node<int>(90),compare_int);
            Add<int>(root,new Node<int>(96),compare_int);
            Add<int>(root,new Node<int>(93),compare_int);
            Add<int>(root,new Node<int>(94),compare_int);
            Add<int>(root,new Node<int>(80),compare_int);
            Add<int>(root,new Node<int>(84),compare_int);
            Add<int>(root,new Node<int>(82),compare_int);
            Add<int>(root,new Node<int>(81),compare_int);
            Add<int>(root, new Node<int>(78),compare_int);

            bool test = test_tools(); // first test the test tools

            if (test) test = (null != get_successor<int>(Find<int>(root, -30, compare_int)));
            if (test) test = (get_successor<int>(Find<int>(root,-30,compare_int)).Value == -10); // left sinbling to right parentless sibling

            if (test) test = (null != get_successor<int>(Find<int>(root, 5, compare_int)));
            if (test) test = (get_successor<int>(Find<int>(root, 5, compare_int)).Value == 12); // left sibling to right parent with single left child

            if (test) test = (null != get_successor<int>(Find<int>(root, 10, compare_int)));
            if (test) test = (get_successor<int>(Find<int>(root, 10, compare_int)).Value == 35); // left sibling to right parent with single right child

            if (test) test = (null != get_successor<int>(Find<int>(root, 20, compare_int)));
            if (test) test = (get_successor<int>(Find<int>(root, 20, compare_int)).Value == 50); // left sibling with no right sibling returns parent

            if (test) test = (null != get_successor<int>(Find<int>(root, 50, compare_int)));
            if (test) test = (get_successor<int>(Find<int>(root, 50, compare_int)).Value == 0); // right sibling returns parent

            if (test) test = (null != get_successor<int>(Find<int>(root, 78, compare_int)));
            if (test) test = (get_successor<int>(Find<int>(root, 78, compare_int)).Value == 81); // left sibling to right grand parent with left.left child

            if (test) test = (null != get_successor<int>(Find<int>(root, 80, compare_int)));
            if (test) test = (get_successor<int>(Find<int>(root, 80, compare_int)).Value == 94); // left sibling to right grand parent with left.right child

            if (test) test = (null != get_successor<int>(Find<int>(root, 90, compare_int)));
            if (test) test = (get_successor<int>(Find<int>(root, 90, compare_int)).Value == 107); // left sibling to right grand parent with right.left child

            if (test) test = (null != get_successor<int>(Find<int>(root, 100, compare_int)));
            if (test) test = (get_successor<int>(Find<int>(root, 100, compare_int)).Value == 114); // left sibling to right grand parent with right.right child

            if (test) test = (null == get_successor<int>(Find<int>(root, 70, compare_int))); // sucessor of root is null

            if (test) test = (null != get_successor<int>(Find<int>(root, 75, compare_int)));
            if (test) test = (get_successor<int>(Find<int>(root, 75, compare_int)).Value == 70); // right child of root returns root - not needed



            Console.WriteLine("Tests have {0}", (test) ? "succeeded" : "failed");
            Console.ReadLine();
        }
        // insert order is 70 0 -20 -30 -10 50 20 10 5 30 35 15 12 75 120 110 112 113 114 100 106 108 107 90 96 93 94 80 84 82 81 78
        // successor is defined as the the next item in a binary tree in a post-order retreival.
        // post order 
        // sucessor[node]=>number  [-30]=>-10, [5]=>12,[10]=>35,[20]=>50,[50]=>0,[78]=>81,[80]=>94,[90]=>107,[100]=>114,[70]=>null
        //                                                      70
        //                         /--------------------------/   \-------------------\
        //                        0                                                   75
        //          /------------/ \--------\                                             -------\
        //        -20                        50                                                120
        //     /--/ \---\            /-------/                                       /---------/
        //  -30        -10          20                                             110         
        //                    /----/  \----\                                /-----/  \---------\
        //                  10              30                             100                  112
        //             /---/  \-----\        \---\                 /------/    \----\            \-----\
        //            5             15           35               90                 106                 113
        //                    /-----/                    /-------/   \-------\           \-----\           \------\
        //                   12                        80                     96                108                114
        //                                   /--------/  \-----\       /----/            /-----/
        //                                 70                   84    93               107
        //                                              /------/        \------\         
        //                                             82                       94     
        //                                    /--------/              
        //                                  81                      

        public static Node<T> get_successor<T>(Node<T> n) {
            if (null == n.Parent) return null; // root node has no successor
            // from here on, everyone has a parent
            if (IsRightNode<T>(n)) return n.Parent;  // a right node returns it's parent
            // this node must be a left node
            // if this node has a right node sibling, returns it's left-most descendent
            if (null != n.Parent.Right) return LeftThenRightThenMe<T>(n.Parent.Right);
            // else return the parent
            return n.Parent;
        }
        public static Node<T> LeftThenRightThenMe<T>(Node<T> me) {
            if (null != me.Left) return LeftThenRightThenMe<T>(me.Left);
            if (null != me.Right) return LeftThenRightThenMe<T>(me.Right);
            return me;
        }
        public static bool IsRightNode<T>(Node<T> n) { // n must be non-null, and have a parent
            return (n.Parent.Right == n);
        }
        // to you Java hacks, welcome to a better language.
        public static Func<int, int, int> compare_int = (left, right) => (left == right) ? 0 : (left < right) ? -1 : 1;
        public static Node<T> Find<T>(Node<T> root, T value, Func<T, T, int> compare) {
            Node<T> current = root;
            bool found = false;
            while ((null != current) && (!found)) {
                int cmp = compare(value,current.Value);
                found = (0 == cmp);
                if (!found) current = (cmp < 0) ? current.Left : current.Right;
            }
            return current;
        }
        public static void Add<T>(Node<T> parent, Node<T> new_item, Func<T,T,int> compare) {
            if (0 < compare(parent.Value, new_item.Value)) {
                if (null == parent.Left) { parent.Left = new_item; new_item.Parent = parent; }
                else Add<T>(parent.Left, new_item, compare);
            } else {
                if (null == parent.Right) { parent.Right = new_item; new_item.Parent = parent; }
                else Add<T>(parent.Right, new_item, compare);
            }
        }
       
        public static bool test_tools() {
            bool success = true;
            Node<int> root = new Node<int>(10);
            Node<int> left = new Node<int>(5);
            Node<int> right = new Node<int>(20);
            // add left and right to the root
            Add<int>(root, left, compare_int);
            Add<int>(root, right, compare_int);
            // est that they were added
            success = success && (root.Left != null);
            success = success && (root.Right != null);
            // can Find find the root?
            success = success && (null != Find<int>(root,10,compare_int));
            // is the found node, the root node?
            if (null != Find<int>(root, 10, compare_int)) success = success && (10 == Find<int>(root, 10, compare_int).Value);
            // did Add add the left node?
            if (root.Left != null) {
                success = success && (root.Left == left);
                success = success && (root.Left.Value == 5);
                success = success && (root.Left.Parent == root);
            }
            // did Add add the right node
            if (root.Right != null) {
                success = success && (root.Right == right);
                success = success && (root.Right.Value == 20);
                success = success && (root.Right.Parent == root);
            }
            // can Find find a left node?
            success = success && (null != Find<int>(root, 5, compare_int));
            // can Find find a right node?
            success = success && (null != Find<int>(root, 20, compare_int));
            // did Find find the correct nodes
            if (null != Find<int>(root, 5, compare_int)) success = success && (5 == Find<int>(root, 5, compare_int).Value);
            if (null != Find<int>(root, 20, compare_int)) success = success && (20 == Find<int>(root, 20, compare_int).Value);




            return success;
        }
    }
}
