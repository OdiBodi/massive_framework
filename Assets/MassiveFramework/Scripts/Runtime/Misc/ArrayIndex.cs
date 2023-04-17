namespace MassiveCore.Framework.Runtime
{
    public class ArrayIndex
    {
        private readonly int _index;
        private readonly int _columns;

        public ArrayIndex(int index, int columns)
        {
            _index = index;
            _columns = columns;
        }

        public int RowIndex => _index / _columns;
        public int ColumnIndex => _index % _columns;

        public int RowLinearIndex(int index)
        {
            return RowIndex * _columns + index;
        }

        public int ColumnLinearIndex(int index)
        {
            return index * _columns + ColumnIndex;
        }
    }
}
