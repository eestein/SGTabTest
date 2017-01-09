using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace TabTest
{
	public partial class ResultsTableViewController : UITableViewController
	{
		public List<string> FilteredItems { get; set; }

		public ResultsTableViewController()
		{
			FilteredItems = new List<string>();
		}

		public ResultsTableViewController(IntPtr handle) : base(handle)
		{
			FilteredItems = new List<string>();
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return FilteredItems.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var item = FilteredItems[indexPath.Row];
			var cell = new UITableViewCell(UITableViewCellStyle.Subtitle, "tableCell");

			cell.TextLabel.Text = item;

			return cell;
		}
	}
}
