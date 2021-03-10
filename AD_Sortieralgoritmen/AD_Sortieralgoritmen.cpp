// AD_Sortieralgoritmen.cpp : Diese Datei enthält die Funktion "main". Hier beginnt und endet die Ausführung des Programms.
//

#include <iostream>

int MaxTeilsum4(int a[], int f, int l) {
    //mitte finden
    int n = l - f + 1;

    if (n == 1) {
        return a[f];
    }
    else {
        int newn = (n % 2 == 0 ? n / 2 : n / 2 + 1);

            int MaxBorderSum1 = a[f + newn - 1], i = f + newn - 2, currVal = MaxBorderSum1;
            while (i >= f) {
                currVal += a[i];
                if (currVal > MaxBorderSum1) {
                    MaxBorderSum1 = currVal;
                }
                i--;
            }

            int MaxBorderSum2 = a[f + newn]; 
            i = f + newn + 1, currVal = MaxBorderSum2;
            while (i <= l) {
                currVal += a[i];
                if (currVal > MaxBorderSum2) {
                    MaxBorderSum2 = currVal;
                }
                i++;
            }

            return std::max(MaxTeilsum4(a, f, f + newn - 1),
                std::max(MaxTeilsum4(a, f + newn, l), MaxBorderSum1 + MaxBorderSum2));
            
    }
}

void ausgeben(int a[], int n) {
    for (int i = 0; i < n; i++) {
        std::cout << a[i] << "  ";
    }
    std::cout << "\n";
}

void ausgeben2(int a[],int start, int n) {
    for (int i = start; i < n; i++) {
        std::cout << a[i] << "  ";
    }
    std::cout << "\n";
}



void SelectionSort(int a[], int n) {
    int i, j, min;
    //so oft wie das array groß ist durchlaufen
    for (i = 0; i < n; i++) {
        min = i;
            //das kleinste Element im noch unsortierten Array suchen
            for (j = i; j < n;j++) {
                if (a[j] < a[min]) min = j;
            }
            // Das kleinste mit dem minimalen Element vertauschen
            int h = a[i];
            a[i] = a[min];
            a[min] = h;
    }
}

void BubbleSort(int a[], int n) {
    // Läuft duch und schiebt den kleinsten wert immer nach vorne
    for (int i = 0; i < n; i++) {
        for (int j = n - 2; j >= i; j--) {
            if (a[j] > a[j + 1]) {
                int h = a[j];
                a[j] = a[j+1];
                a[j + 1] = h;

            }
        }
    }
}

void InsertionSort(int a[], int n) {
    int i, j, key;

    //läuft alle objekte durch
    for (j = 1; j < n;j++) {
        //key ist das aktuell einzusortierende objekt
        key = a[j];
        //i ist nun das objekt davor
        i = j - 1;
        //Diese Schleife läuft so oft durch, dass i das 0 objekt des Arrays ist, oder bis das einzusortierende objekt größer ist als das vorherige
        while (i >= 0 && a[i] > key) {
            a[i + 1] = a[i];
            i = i - 1;
        }
        // das folgende objekte anschauen und einsortieren
        a[i + 1] = key;
         ausgeben(a, n);
    }
}

void swap(int& a, int& b) {
    int h = b;
    b = a;
    a = h;
}


void PreparePartition(int a[], int start,int ende, int  &p) {

    int pivot = a[start];
    p = start - 1;
    for (int i = start; i <= ende; i++) {
        if (a[i] <= pivot) {
            p++;
            swap(a[i], a[p]);
        }
    }
    swap(a[start], a[p]);
    ausgeben2(a, start ,ende);
}


void Quicksort(int a[], int start, int ende) {
    int part;
    if (start < ende) {
        PreparePartition(a, start, ende, part);
        std::cout << "Erster Teil";
        ausgeben2(a, start, part - 1);
        Quicksort(a, start, part - 1);
        std::cout << "Zweiter Teil";
        Quicksort(a, part+1, ende);
    }
  }

void Merge(int a[], int f, int l, int m) {
    int i;
    int n = l - f + 1;
    int a1f = f, a1l = m - 1;
    int a2f = m, a2l = l;
    int* anew = new int[n];
    for (i = 0;i < n; i++)
    {
        if (a1f <= a1l) {
            if (a2f <= a2l)
            {
                if (a[a1f] <= a[a2f]) anew[i] = a[a1f++];
                else anew[i] = a[a2f++];
            }
            else anew[i] = a[a1f++];
        }
        else anew[i] = a[a2f++];
    }
    for (i = 0;i < n;i++) a[f + i] = anew[i];
    delete[] anew;
}


void MergeSort(int a[], int f, int l)
{
    if (f < l) {
        int m = (f + l + 1) / 2;
        MergeSort(a, f, m - 1);
        MergeSort(a, m, l);
        Merge(a, f, l, m);
    }
}

void Heapify(int a[], int f, int l, int root)
{
    int largest, left = f + (root - f) * 2 + 1, right = f + (root - f) * 2 + 2;
    if (left <= l && a[left] > a[root]) largest = left;
    else largest = root;
    if (right <= l && a[right] > a[largest]) largest = right;
    if (largest != root) {
        swap(a[root], a[largest]);
        Heapify(a, f, l, largest);
    }
}

void BuildHeap(int a[], int f, int l){
    int n = l - f + 1;
    for (int i = f + (n - 2) / 2;i >= f;i--) {
        Heapify(a, f, l, i);
    }
}

void HeapSort(int a[], int f, int l) {
    BuildHeap(a, f, l);
    
    for (int i = l; i > f;i--) {
        swap(a[f], a[i]);
        Heapify(a, f, i - 1, f);
    }
}

void CountSort(int a[], int n, int k)
{
    int i, j = 1, * bin = new int[k + 1];
    for (i = 1;i <= k;i++) bin[i] = 0;
    for (i = 0;i < n;i++) bin[a[i]]++;
    for (i = 0;i < n;i++)
    {
        while (bin[j] == 0) j++;
        a[i] = j; bin[j]--;
    }
    delete[] bin;
}

void MapSort(int a[], int n, double c) {
    int newn = (int)((double)n * c), i, j = 0;
    int* bin = new int[newn], max = INT16_MAX, min = INT16_MIN;
    for (i = 0;i < newn;i++) bin[i] = -1;
    for (i = 0;i < n;i++) {
        if (a[i] < min) min = a[i];
        if (a[i] > max) max = a[i];
    }
    double dist = (double)(max - min) / (double)(newn - 1);
    for (i = 0;i < n;i++) {
        int t = (int)((double)(a[i] - min) / dist), insert = a[i], left = 0;
        if (bin[t] != -1 && insert <= bin[t]) left = 1;
        while (bin[t] != -1) {
            if (left == 1) {
                if (insert > bin[t]) swap(bin[t], insert);
                if (t > 0) t--; else left = 0;
            }
            else {
                if (insert <= bin[t]) swap(bin[t], insert);
                if (t < newn - 1) t++; else left = 1;
            }
        }
        bin[t] = insert;
    }
    for (i = 0;i < newn;i++) if (bin[i] != -1) a[j++] = bin[i];
    delete[] bin;
}


int main()
{
    int a[4] = { 1,3,2,4};

   //std::cout << MaxTeilsum4(a, 0, 9);
   //InsertionSort(a, 5);
    Quicksort(a, 0, 3);

    //MergeSort(a, 0, 4);

    //HeapSort(a, 0, 4);

    //CountSort(a, 5, 12);

    //MapSort(a, 5, 2.5);

    ausgeben(a, 4);

   //Quicksort(a, 0, 9);
    std::cout << "Hello World!\n";
}

