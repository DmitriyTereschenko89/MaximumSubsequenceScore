namespace MaximumSubsequenceScore
{
    internal class Program
    {
        public class MaximumSubsequenceScore
        {
            private class Pair
            {
                public int num1;
                public int num2;

                public Pair(int num1, int num2)
                {
                    this.num1 = num1;
                    this.num2 = num2;
                }
            }

            private class MinHeap
            {
                private readonly List<int> heap;

                private void Swap(int i, int j)
                {
                    (heap[i], heap[j]) = (heap[j], heap[i]);
                }

                private void SiftDown(int currentIdx, int endIdx)
                {
                    int childOneIdx = currentIdx * 2 + 1;
                    while (childOneIdx <= endIdx)
                    {
                        int swapIdx = childOneIdx;
                        int childTwoIdx = currentIdx * 2 + 2;
                        if (childTwoIdx <= endIdx && heap[childTwoIdx] < heap[childOneIdx])
                        {
                            swapIdx = childTwoIdx;
                        }
                        if (heap[swapIdx] < heap[currentIdx])
                        {
                            Swap(swapIdx, currentIdx);
                            currentIdx = swapIdx;
                            childOneIdx = currentIdx * 2 + 1;
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                private void SiftUp(int currentIdx)
                {
                    int parentIdx = (currentIdx - 1) / 2;
                    while(parentIdx >= 0 && heap[parentIdx] > heap[currentIdx])
                    {
                        Swap(parentIdx, currentIdx);
                        currentIdx = parentIdx;
                        parentIdx = (currentIdx - 1) / 2;
                    }
                }

                public MinHeap()
                {
                    heap = new List<int>();
                }

                public int Count()
                {
                    return heap.Count;
                }

                public void Add(int value)
                {
                    heap.Add(value);
                    SiftUp(heap.Count - 1);
                }

                public int Peek()
                {
                    return heap[0];
                }

                public int Remove()
                {
                    Swap(0, heap.Count - 1);
                    int removed = heap[^1];
                    heap.RemoveAt(heap.Count - 1);
                    SiftDown(0, heap.Count - 1);
                    return removed;
                }
            }

            public long MaxScore(int[] nums1, int[] nums2, int k)
            {
                int n = nums1.Length;
                Pair[] pairs = new Pair[n];
                for (int i = 0; i < n; ++i)
                {
                    pairs[i] = new Pair(nums1[i], nums2[i]);
                }
                Array.Sort(pairs, (a, b) => b.num2 - a.num2);
                long maxScore = 0;
                long sumK = 0;
                MinHeap minHeap = new();
                foreach(Pair pair in pairs)
                {
                    sumK += pair.num1;
                    minHeap.Add(pair.num1);
                    if (minHeap.Count() > k)
                    {
                        sumK -= minHeap.Remove();
                    }
                    if (minHeap.Count() == k)
                    {
                        maxScore = Math.Max(maxScore, sumK * pair.num2);
                    }
                }
                return maxScore;
            }
        }

        static void Main(string[] args)
        {
            MaximumSubsequenceScore maximumSubsequenceScore = new();
            Console.WriteLine(maximumSubsequenceScore.MaxScore(new int[] { 1, 3, 3, 2 }, new int[] { 2, 1, 3, 4 }, 3));
            Console.WriteLine(maximumSubsequenceScore.MaxScore(new int[] { 4, 2, 3, 1, 1 }, new int[] { 7, 5, 10, 9, 6 }, 1));
        }
    }
}