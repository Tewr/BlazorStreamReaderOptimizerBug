using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StreamReaderRepro
{
    public class StringStream : Stream
    {
        private byte[] _bytes;

        public StringStream(string source, Encoding encoding)
        {
            _bytes = encoding.GetBytes(source);
        }

        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => false;

        public override long Length => _bytes.Length;

        public override long Position { get; set; }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            var actualCount = count;
            if (Position + actualCount > Length)
            {
                actualCount = (int)(Length - Position);
            }
            Array.Copy(_bytes, Position, buffer, offset, actualCount);

            Position += actualCount;

            return actualCount;
        }
    }
}
