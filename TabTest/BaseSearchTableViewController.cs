using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;

namespace TabTest
{
	public class BaseSearchTableViewController : UITableViewController
	{
		private ResultsTableViewController resultsTableViewController;
		private UISearchController searchController;
		private bool searchControllerWasActive;
		private bool searchControllerSearchFieldWasFirstResponder;
		
		public List<string> Items { get; set; }

		public BaseSearchTableViewController()
		{
			Items = new List<string>();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			resultsTableViewController = new ResultsTableViewController();
			searchController = new UISearchController(resultsTableViewController)
			{
				WeakDelegate = this,
				DimsBackgroundDuringPresentation = false,
				WeakSearchResultsUpdater = this,
				HidesNavigationBarDuringPresentation = false,
				ExtendedLayoutIncludesOpaqueBars = true
			};
			searchController.SearchBar.SizeToFit();
			TableView.TableHeaderView = searchController.SearchBar;
			resultsTableViewController.TableView.WeakDelegate = this;
			searchController.SearchBar.WeakDelegate = this;

			if (searchControllerWasActive)
			{
				searchController.Active = searchControllerWasActive;
				searchControllerWasActive = false;

				if (searchControllerSearchFieldWasFirstResponder)
				{
					searchController.SearchBar.BecomeFirstResponder();
					searchControllerSearchFieldWasFirstResponder = false;
				}
			}

			TableView.TableFooterView = new UIView();
		}

		[Export("searchBarSearchButtonClicked:")]
		public virtual void SearchButtonClicked(UISearchBar searchBar)
		{
			searchBar.ResignFirstResponder();
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return Items.Count;
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		[Export("updateSearchResultsForSearchController:")]
		public virtual void UpdateSearchResultsForSearchController(UISearchController searchController)
		{
			var tableController = (ResultsTableViewController)searchController.SearchResultsController;

			tableController.FilteredItems = PerformSearch(searchController.SearchBar.Text);
			tableController.TableView.ReloadData();
		}

		List<string> PerformSearch(string searchString)
		{
			searchString = searchString.Trim();

			var searchItems = string.IsNullOrEmpty(searchString)
				? new string[0]
				: searchString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			var filteredItems = new List<string>();

			foreach (var item in searchItems)
			{
				var query = Items.Where(i => i.IndexOf(item, StringComparison.OrdinalIgnoreCase) >= 0)
								 .OrderBy(i => i);

				filteredItems.AddRange(query);
			}

			return filteredItems.Distinct().ToList();
		}
	}
}
