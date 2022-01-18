using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebasePusher.Interfaces
{
  public interface IPlatformSpecificPushMessage
  {
    string Platform { get; }
  }
}
