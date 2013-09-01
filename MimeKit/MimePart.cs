//
// MimePart.cs
//
// Author: Jeffrey Stedfast <jeff@xamarin.com>
//
// Copyright (c) 2013 Jeffrey Stedfast
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System;

namespace MimeKit {
	public class MimePart : MimeEntity
	{
		static readonly StringComparer icase = StringComparer.OrdinalIgnoreCase;
		ContentEncoding encoding;

		public MimePart (string mediaType, string mediaSubtype) : base (mediaType, mediaSubtype)
		{
		}

		internal MimePart (HeaderList headers, ContentType type) : base (headers, type)
		{
		}

		public ContentEncoding ContentTransferEncoding {
			get { return encoding; }
			set {
				if (encoding == value)
					return;

				Headers.Changed -= OnHeadersChanged;

				encoding = value;
				switch (encoding) {
				case ContentEncoding.Default:
					Headers.Remove ("Content-Transfer-Encoding");
					break;
				case ContentEncoding.SevenBit:
					Headers["Content-Transfer-Encoding"] = "7bit";
					break;
				case ContentEncoding.EightBit:
					Headers["Content-Transfer-Encoding"] = "8bit";
					break;
				case ContentEncoding.Binary:
					Headers["Content-Transfer-Encoding"] = "binary";
					break;
				case ContentEncoding.Base64:
					Headers["Content-Transfer-Encoding"] = "base64";
					break;
				case ContentEncoding.QuotedPrintable:
					Headers["Content-Transfer-Encoding"] = "quoted-printable";
					break;
				case ContentEncoding.UUEncode:
					Headers["Content-Transfer-Encoding"] = "x-uuencode";
					break;
				}

				Headers.Changed += OnHeadersChanged;
			}
		}

		public ContentObject ContentObject {
			get; set;
		}

		// FIXME: implement the rest
	}
}
