using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DMS.MapLibrary
{
    public class TreeViewNoImage : TreeView
    {
        public const int NOIMAGE = -1;

        public TreeViewNoImage()
            : base()
        {
            // .NET Bug: Unless LineColor is set, Win32 treeview returns -1 (default), .NET returns Color.Black!
            base.LineColor = SystemColors.GrayText;

            base.DrawMode = TreeViewDrawMode.OwnerDrawAll;
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            const int SPACE_IL = 3;  // space between Image and Label

            // we only do additional drawing
            e.DrawDefault = true;

            base.OnDrawNode(e);

            if (base.ShowLines && base.ImageList != null && e.Node.ImageIndex == NOIMAGE
                // exclude root nodes, if root lines are disabled
                //&& (base.ShowRootLines || e.Node.Level > 0))
                )
            {
                // Using lines & images, but this node has none: fill up missing treelines

                // Image size
                int imgW = base.ImageList.ImageSize.Width;
                int imgH = base.ImageList.ImageSize.Height;

                // Image center
                int xPos = e.Node.Bounds.Left - SPACE_IL - imgW / 2;
                int yPos = (e.Node.Bounds.Top + e.Node.Bounds.Bottom) / 2;

                // Image rect
                Rectangle imgRect = new Rectangle(xPos, yPos, 0, 0);
                imgRect.Inflate(imgW / 2, imgH / 2);

                using (Pen p = new Pen(base.LineColor, 1))
                {
                    p.DashStyle = DashStyle.Dot;

                    // account uneven Indent for both lines
                    p.DashOffset = base.Indent % 2;

                    // Horizontal treeline across width of image
                    // account uneven half of delta ItemHeight & ImageHeight
                    int yHor = yPos + ((base.ItemHeight - imgRect.Height) / 2) % 2;

                    //if (base.ShowRootLines || e.Node.Level > 0)
                    //{
                    //    e.Graphics.DrawLine(p, imgRect.Left, yHor, imgRect.Right, yHor);
                    //}
                    //else
                    //{
                    //    // for root nodes, if root lines are disabled, start at center
                    //    e.Graphics.DrawLine(p, xPos - (int)p.DashOffset, yHor, imgRect.Right, yHor);
                    //}

                    e.Graphics.DrawLine(p,
                        (base.ShowRootLines || e.Node.Level > 0) ? imgRect.Left : xPos - (int)p.DashOffset,
                        yHor, imgRect.Right, yHor);


                    if (!base.CheckBoxes && e.Node.IsExpanded)
                    {
                        // Vertical treeline , offspring from NodeImage center to e.Node.Bounds.Bottom
                        // yStartPos: account uneven Indent and uneven half of delta ItemHeight & ImageHeight
                        int yVer = yHor + (int)p.DashOffset;
                        e.Graphics.DrawLine(p, xPos, yVer, xPos, e.Node.Bounds.Bottom);
                    }
                }
            }
        }

        protected override void OnAfterCollapse(TreeViewEventArgs e)
        {
            base.OnAfterCollapse(e);

            if (!base.CheckBoxes && base.ImageList != null && e.Node.ImageIndex == NOIMAGE)
            {
                // DrawNode event not raised: redraw node with collapsed treeline
                base.Invalidate(e.Node.Bounds);
            }
        }

    }
}
