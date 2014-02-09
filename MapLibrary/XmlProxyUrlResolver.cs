using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;

namespace DMS.MapLibrary
{
    /// <summary>
    /// XmlProxyUrlResolver Class
    /// </summary>
    public class XmlProxyUrlResolver : XmlResolver
    {
        ICredentials serverCredentials;
        ICredentials proxyCredentials;
        IWebProxy proxy;
        string proxyType = "http";

        public IWebProxy Proxy
        {
            get
            {
                return proxy;
            }
            set
            {
                proxy = value;
            }
        }

        public ICredentials GetCredentials()
        {
            return serverCredentials;
        }

        public ICredentials GetProxyCredentials()
        {
            return proxyCredentials;
        }

        public string ProxyType
        {
            get { return proxyType; }
            set { proxyType = value; }
        }

        public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
        {
            WebRequest req = WebRequest.Create(absoluteUri);
            req.Proxy = proxy;
            req.Credentials = serverCredentials;
            return req.GetResponse().GetResponseStream();
        }


        public override ICredentials Credentials
        {
            set { serverCredentials = value; }
        }

        public ICredentials ProxyCredentials
        {
            set { proxyCredentials = value; }
        }
    }
}
