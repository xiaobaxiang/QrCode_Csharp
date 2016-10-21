namespace Gma.QrCodeNet.Encoding.Positioning.Stencils
{
    using Gma.QrCodeNet.Encoding;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal abstract class PatternStencilBase : BitMatrix
    {
        protected const bool o = false;
        protected const bool x = true;

        internal PatternStencilBase(int version)
        {
            this.Version = version;
        }

        public abstract void ApplyTo(TriStateMatrix matrix);

        public override int Height
        {
            get
            {
                return this.Stencil.GetLength(1);
            }
        }

        public override bool[,] InternalArray
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool this[int i, int j]
        {
            get
            {
                return this.Stencil[i, j];
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public abstract bool[,] Stencil { get; }

        public int Version { get; private set; }

        public override int Width
        {
            get
            {
                return this.Stencil.GetLength(0);
            }
        }
    }
}

