using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ePlaces.BusinessLayer.Common.Interfaces {
	public interface IImageProcessing {
		public Stream Scale(Stream file, double coefficient);
	}
}
