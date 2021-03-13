using System;
using System.Collections.Generic;
using System.Text;

namespace Aufgabe3
{
    class AvlTree
    {
        private AvlElement root;

        public int Max(int a, int b)
        {
            if (a > b) return a; else return b;
        }

        public int GetHeight(AvlElement element)
        {
            if(element == null)
            {
                return -1;
            }
            else
            {
                return element.height;
            }

        }

        public AvlElement UpdateHeight(AvlElement element)
        {
            element.height = 1 + Max(GetHeight(element.left), GetHeight(element.right));
            return element;
        }

        public AvlElement RotateLeft(AvlElement element)
        {
            AvlElement b = element.right;

            element.right = b.left; b.left = element; element = b;

            UpdateHeight(element.left); UpdateHeight(element);
            return element;
        }


        public AvlElement RotateRight(AvlElement element)
        {
            AvlElement b = element.left;

            element.left = b.right; b.right = element; element = b;

            UpdateHeight(element.right); UpdateHeight(element);
            return element;
        }

        public AvlElement DoubleRotationLeft(AvlElement element)
        {
            element.right =  RotateRight(element.right);
            element = RotateLeft(element);

            return element;
        }

        public AvlElement DoubleRotationRight(AvlElement element)
        {
            element.left = RotateLeft(element.left);
            element = RotateRight(element);
            return element;
        }

        public AvlElement CheckRotationRight(AvlElement element)
        {
            if(element != null)
            {
                if(element.left != null)
                {
                    if (GetHeight(element.left) - GetHeight(element.right) == 2)
                    {
                        if(GetHeight(element.left.right) > GetHeight(element.left.left))
                        {
                           element =  DoubleRotationRight(element);
                        }
                        else
                        {
                            element = RotateRight(element);
                        }
                    }else
                    {
                        element = UpdateHeight(element);
                    }
                }
                else
                {
                    element = UpdateHeight(element);
                }
            }

            return element;
        }

        public AvlElement CheckRotationLeft(AvlElement element)
        {
            if (element != null)
            {
                if (element.right != null)
                {
                    if (GetHeight(element.right) - GetHeight(element.left) == 2)
                    {
                        if (GetHeight(element.right.left) > GetHeight(element.right.right))
                        {
                          element =  DoubleRotationLeft(element);
                        }
                        else
                        {
                          element = RotateLeft(element);
                        }
                    }
                    else
                    {
                        element = UpdateHeight(element);
                    }
                }
                else
                {
                    element = UpdateHeight(element);
                }
            }

            return element;
        }

        public void Insert(int o)
        {
           root =  Insert(root, o);
        }

        public AvlElement Insert(AvlElement element, int o)
        {
            if(element == null)
            {
                element = new AvlElement();
                element.height = 0; element.value = o;  
                element.left = null;
                element.right = null;
            }
            else
            {
                if (o <= element.value)
                {
                    element.left = Insert(element.left, o);
                   element =  CheckRotationRight(element);
                }
                else
                {
                    element.right = Insert(element.right, o);
                    element = CheckRotationLeft(element);
                }
            }

            return element;
        }

        public void ausgeben()
        {
            ausgeben(root);
            Console.WriteLine("");
        }

        public void ausgeben(AvlElement element)
        {
            if(element != null)
            {
                Console.Write("(" + element.value + ",");
                ausgeben(element.left);
                Console.Write(",");
                ausgeben(element.right);
                Console.Write(")");
            }
            else
            {
                Console.Write("n");
            }
        }

        public void  delete(int o)
        {
           root = DeleteRekursiv(root, o);
        }

        public AvlElement DeleteRekursiv(AvlElement element, int o)
        {
            if(element != null)
            {
                if (o < element.value)
                {
                    element.left = DeleteRekursiv(element.left, o);
                    element = CheckRotationLeft(element);
                }
                else if(o > element.value)
                {
                    element.right = DeleteRekursiv(element.right, o);
                    element = CheckRotationRight(element);
                }

                //Element gefunden!
                if(element.value == o)
                {
                    if (element.left == null && element.right == null)
                    {
                        element = null;
                    }
                    else
                if (element.left == null && element.right != null)
                    {
                        element = element.right;
                    }
                    else if (element.left != null && element.right == null)
                    {
                        element = element.left;
                    }
                    else if (element.left != null && element.right != null)
                    {

                        AvlElement current = element; // kopie des knotens 

                        element = findminValueElement(element.left);
                        //Inorder Nachfolger finden!!

                        current.value = element.value;
                        element = null;
                    }
                }
                return element;
            }
            return null;
        }

        public AvlElement findminValueElement(AvlElement element)
        {
           if(element.right != null)
            {
                element.right = findminValueElement(element.right);
            }
            return element;
        }

        public AvlElement DeleteIterativ(AvlElement element, int o)
        {
            // Teilbaum mit Wurzel elem vorhanden?
            if (element != null)
            {
                // Zu entfernenden Knoten mit Wert o finden
                AvlElement curr = element;
                AvlElement parent = curr;
                Stack<AvlElement> elements = new Stack<AvlElement>();
                Stack<bool> teilbaumrechts = new Stack<bool>();

                while(curr.value != o)
                {
                    elements.Push(curr);
                    if(o <= element.value)
                    {
                        teilbaumrechts.Push(false);
                        parent = curr;
                        curr = curr.left;
                    }
                    else
                    {
                        teilbaumrechts.Push(true);
                        parent = curr;
                        curr = curr.right;
                    }
                }
                // Nachfolger prüfen
                // Entsprechend der Nachfolger
                // Knoten umhängen, AVL-Eigenschaft prüfen

                //Sonderfall, falls root gelöscht wird und damit normalerweise kein parent existiert
                if(curr == element)
                {
                    parent = new AvlElement();
                    parent.right = curr;
                    parent.value = Int16.MinValue;
                }

                if (curr.left == null && curr.right == null)
                {
                    if (curr.value <= parent.value)
                    {
                        parent.left = null;
                    }
                    else
                    {
                        parent.right = null;
                    }
                    
                }
                else
                if(curr.left == null && curr.right != null) {
                    if (curr.value <= parent.value)
                    {
                        parent.left = curr.right;
                    }
                    else
                    {
                        parent.right = curr.right;
                    }
                } 
                else if(curr.left !=  null && curr.right == null)
                {
                    if (curr.value <= parent.value)
                    {
                        parent.left = curr.left;
                    }
                    else
                    {
                        parent.right = curr.left;
                    }
                }
                else if(curr.left != null && curr.right != null)
                {
                    //Inorder Nachfolger finden!!

                    curr = curr.right;
                    AvlElement curr2 = element;

                    parent = element; // Achtung!!
                    curr2 = curr2.left;

                    while(curr2.right != null)
                    {
                        parent = curr2;
                        curr2 = curr2.right;
                    }

                    curr.value = curr2.value; // werte vertauschen
                    // curr2 löschen
                    parent.right = null;
                    
                }

                //Sonderfall, fall root gelöscht wird
                if(element == curr)
                {
                    element = parent.right;
                }

              //  AvlElement parent;
                while(elements.Count != 0)
                {
                    parent = elements.Pop();
                    parent = UpdateHeight(parent);
                    
                    if(teilbaumrechts.Pop() == false)
                    {
                     element = CheckRotationLeft(parent);
                    }
                    else
                    {
                     element = CheckRotationRight(parent);
                    }

                }

                // Knoten löschen, Höhen aktualisieren
            }
            return element;
        }

    }
}
