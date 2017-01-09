using System;
using Foundation;
using UIKit;

namespace TabTest
{
	public class STableViewController : BaseSearchTableViewController
	{
		public int Number { get; set; }

		public STableViewController()
		{
			Items = new System.Collections.Generic.List<string>
			{
				"Item 1",
				"Khyj 2",
				"Zoof 3",
				"Akqi 4",
				"Oplo 5",
				"Aboo 6"
			};
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = "Page 2";
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var item = Items[indexPath.Row];
			var cell = new UITableViewCell(UITableViewCellStyle.Subtitle, "tableCell");

			cell.TextLabel.Text = item;

			return cell;
		}
	}
}
