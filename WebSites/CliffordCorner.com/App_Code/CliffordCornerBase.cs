using System;
using System.Diagnostics;

/// <summary>
/// Base methods used throughout the site.
/// </summary>
public abstract class CliffordCornerBase
{
    protected void DebugMessage(string message)
    {
        Debug.WriteLine(string.Format("{0}: {1}", this.GetType().FullName, message));
    }

    protected void DebugException(string message, Exception e)
    {
        this.DebugMessage(message);

        Exception x = e;

        Debug.Indent();
        while (x != null)
        {
            this.DebugMessage(x.StackTrace);
            x = x.InnerException;
        }
        Debug.Unindent();
    }
}