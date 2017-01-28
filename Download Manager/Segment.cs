namespace Download_Manager
{
    public class Segment
    {
        private long _StartPoint, _EndPoint;

        public long StartPoint
        {
            get
            {
                return _StartPoint;
            }
        }

        public long EndPoint
        {
            get
            {
                return _EndPoint;
            }
        }

        public Segment(long StartPoint, long EndPoint)
        {
            _StartPoint = StartPoint;
            _EndPoint = EndPoint;
        }

        public static Segment[] CalculateSegments(int countSegments, long contentLength)
        {
            long startPosition = 0, minimumSegmentSize = 1024, segmentSize = contentLength / (long)countSegments;
            
            Segment[] segments = new Segment[countSegments];

            while (countSegments > 1 && segmentSize < minimumSegmentSize)
            {
                countSegments--;
                segmentSize = contentLength / (long)segments.Length;
            }

            for (int i = 0; i < segments.Length; i++)
            {
                if (i == segments.Length - 1)
                {
                    segments[i] = new Segment(startPosition, contentLength);
                }
                else
                {
                    segments[i] = new Segment(startPosition, startPosition + segmentSize);
                }

                startPosition = segments[i].EndPoint + 1;
            }

            return segments;
        }
    }
}