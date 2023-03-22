﻿using System;
using System.Linq;
using Windows.UI;
using System.Threading.Tasks;
using Private.Infrastructure;
using Uno.UI.RuntimeTests.Helpers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TreeView = Microsoft.UI.Xaml.Controls.TreeView;
using TreeViewItem = Microsoft.UI.Xaml.Controls.TreeViewItem;
using Uno.UI.RuntimeTests.Tests.Windows_UI_Xaml_Controls.TreeViewTests;
using System.Collections.Generic;

namespace Uno.UI.RuntimeTests.Tests.Windows_UI_Xaml_Controls;

#if __MACOS__
[Ignore("Currently fails on macOS, part of #9282 epic")]
#endif
#if __IOS__
[Ignore("Test is unstable on iOS currently")]
#endif
[TestClass]
[RunsOnUIThread]
public class Given_TreeView
{
	[TestMethod]
	public async Task When_Open_Close_Twice()
	{
		var SUT = new When_Open_Close_Twice();
		TestServices.WindowHelper.WindowContent = SUT;

		var root = new MyNode();
		root.Name = "root";
		root.Children = new List<MyNode>();
		var child1 = new MyNode { Name = "Child 1" };
		var child2 = new MyNode { Name = "Child 2" };
		root.Children.Add(child1);
		root.Children.Add(child2);

		SUT.myTree.ItemsSource = new[] { root };
		await TestServices.WindowHelper.WaitForIdle();

		var rootNode = (TreeViewItem)SUT.myTree.ContainerFromItem(root);
		rootNode.IsExpanded = true;
		await TestServices.WindowHelper.WaitForIdle();

		var child1Node = (TreeViewItem)SUT.myTree.ContainerFromItem(child1);
		Assert.IsNotNull(child1Node);
		Assert.AreEqual("Child 1", child1Node.Content);

		rootNode.IsExpanded = false;
		await TestServices.WindowHelper.WaitForIdle();

		rootNode.IsExpanded = true;
		await TestServices.WindowHelper.WaitForIdle();

		await TestServices.WindowHelper.WaitFor(() => (SUT.myTree.ContainerFromItem(child1) as TreeViewItem)?.Content?.ToString() == child1.Name);
		await TestServices.WindowHelper.WaitFor(() => (SUT.myTree.ContainerFromItem(child2) as TreeViewItem)?.Content?.ToString() == child2.Name);

		TreeViewItem child1NodeAfter = (TreeViewItem)SUT.myTree.ContainerFromItem(child1);
		Assert.IsNotNull(child1NodeAfter);

		Assert.AreEqual("Child 1", child1NodeAfter.Content);

		var child2NodeAfter = (TreeViewItem)SUT.myTree.ContainerFromItem(child2);
		await TestServices.WindowHelper.WaitForLoaded(child2NodeAfter);
		Assert.IsNotNull(child2NodeAfter);

		Assert.AreEqual("Child 2", child2NodeAfter.Content);
	}

	[TestMethod]
	public async Task When_Open_Close_Twice_Grid()
	{
		var SUT = new When_Open_Close_Twice_Grid();
		TestServices.WindowHelper.WindowContent = SUT;

		var root = new MyNode();
		root.Name = "root 4";
		root.IsDirectory = true;
		root.Children = new List<MyNode>();
		var child1 = new MyNode { Name = "Child 1" };
		var child2 = new MyNode { Name = "Child 2" };
		root.Children.Add(child1);
		root.Children.Add(child2);

		SUT.myTree.ItemsSource = new[] { root };
		await TestServices.WindowHelper.WaitForIdle();

		var rootNode = (TreeViewItem)SUT.myTree.ContainerFromItem(root);
		rootNode.IsExpanded = true;
		await TestServices.WindowHelper.WaitForIdle();

		var child1Node = (TreeViewItem)SUT.myTree.ContainerFromItem(child1);
		Assert.IsNotNull(child1Node);
		Assert.AreEqual(child1, child1Node.DataContext);

		rootNode.IsExpanded = false;
		await TestServices.WindowHelper.WaitForIdle();

		rootNode.IsExpanded = true;
		await TestServices.WindowHelper.WaitForIdle();

		await TestServices.WindowHelper.WaitFor(() => (SUT.myTree.ContainerFromItem(child1) as TreeViewItem)?.DataContext == child1);
		await TestServices.WindowHelper.WaitFor(() => (SUT.myTree.ContainerFromItem(child2) as TreeViewItem)?.DataContext == child2);

		await TestServices.WindowHelper.WaitFor(() => (SUT.myTree.ContainerFromItem(child1) as TreeViewItem)?.Content is Grid);
		await TestServices.WindowHelper.WaitFor(() => (SUT.myTree.ContainerFromItem(child2) as TreeViewItem)?.Content is Grid);

		var child1NodeAfter = (TreeViewItem)SUT.myTree.ContainerFromItem(child1);
		Assert.IsNotNull(child1NodeAfter);

		Assert.AreEqual(child1, child1NodeAfter.DataContext);

		var child2NodeAfter = (TreeViewItem)SUT.myTree.ContainerFromItem(child2);
		await TestServices.WindowHelper.WaitForLoaded(child2NodeAfter);
		Assert.IsNotNull(child2NodeAfter);

		Assert.AreEqual(child2, child2NodeAfter.DataContext);
	}
}

public class MyNode
{
	public string Name { get; set; }

	public bool IsDirectory { get; set; }

	public List<MyNode> Children { get; set; }
}

public class FSObjectTemplateSelector : DataTemplateSelector
{
	public DataTemplate FileTemplate { get; set; }
	public DataTemplate DirectoryTemplate { get; set; }

	protected override DataTemplate SelectTemplateCore(object item, DependencyObject container) => SelectTemplateCore(item);

	protected override DataTemplate SelectTemplateCore(object item)
		=> item is MyNode node && node.IsDirectory
			? DirectoryTemplate
			: FileTemplate;
}
