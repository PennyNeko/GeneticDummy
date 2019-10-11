using System;
using System.Runtime.InteropServices;

namespace GeneticDummy
{
    /**
     * Class containing useful methods to be used in the rest of the code.
     */

    public static class Util
    {
        /**
         * A static method that gets a the row of an array.
         */
        //I shamelessly copy-pasted SO code here...
        public static T[] GetRow<T>(this T[,] array, int row)
        {
            if (!typeof(T).IsPrimitive)
                throw new InvalidOperationException("Not supported for managed types.");

            if (array == null)
                throw new ArgumentNullException("array");

            int cols = array.GetUpperBound(1) + 1;
            T[] result = new T[cols];

            int size;

            if (typeof(T) == typeof(bool))
                size = 1;
            else if (typeof(T) == typeof(char))
                size = 2;
            else
                size = Marshal.SizeOf<T>();

            Buffer.BlockCopy(array, row * cols * size, result, 0, cols * size);

            return result;
        }
    }
}
