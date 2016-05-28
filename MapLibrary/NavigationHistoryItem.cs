using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OSGeo.MapServer;

namespace DMS.MapLibrary
{
    public class NavigationHistoryItem
    {
        private double minx;
        private double miny;
        private double maxx;
        private double maxy;
        public NavigationHistoryItem(rectObj extent)
        {
            minx = extent.minx;
            miny = extent.miny;
            maxx = extent.maxx;
            maxy = extent.maxy;
        }

        public void Apply(mapObj map)
        {
            map.setExtent(minx, miny, maxx, maxy);
        }

        public rectObj GetExtent()
        {
            return new rectObj(minx, miny, maxx, maxy, 0);
        }
    }
}
