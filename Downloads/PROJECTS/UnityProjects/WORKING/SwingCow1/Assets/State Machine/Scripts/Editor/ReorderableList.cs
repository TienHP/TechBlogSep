using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;

[System.Serializable]
public class ReorderableList {
	public delegate void AddCallbackDelegate();
	public ReorderableList.AddCallbackDelegate onAddCallback;
	public delegate void RemoveCallbackDelegate(int index);
	public ReorderableList.RemoveCallbackDelegate onRemoveCallback;
	public delegate void ElementCallbackDelegate(int index);
	public ReorderableList.ElementCallbackDelegate drawElementCallback;
	public delegate void SelectCallbackDelegate(int index);
	public ReorderableList.SelectCallbackDelegate onSelectCallback;
	public delegate void OnHeaderClick();
	public ReorderableList.OnHeaderClick onHeaderClick;
	public delegate void OnBeforeListItems();
	public ReorderableList.OnBeforeListItems onBeforeListItems;

	private string title;
	public IList items;
	private bool draggable;
	private int selectedIndex = -2;
	private bool isDragging;
	private bool displayAdd;
	
	public ReorderableList(string title):this(null,title,false,false){
		
	}
	
	public ReorderableList(IList items, string title,bool draggable):this(items,title,draggable,true){
		
	}
	
	public ReorderableList(IList items, string title,bool draggable, bool displayAdd){
		this.title = title;
		this.items = items;
		this.draggable = draggable;
		this.displayAdd = displayAdd;
	}
	
	public void DoList(){
		if (DoListHeader ()) {
			if(onBeforeListItems != null){
				GUILayout.BeginVertical ((GUIStyle)"PopupCurveSwatchBackground", GUILayout.ExpandWidth (true));
				onBeforeListItems();
				GUILayout.EndVertical();
			}
			DoListItems();
		}
	}
	
	public bool DoListHeader(){
		bool foldOut = EditorPrefs.GetBool (title, false);
		Rect rect = GUILayoutUtility.GetRect (new GUIContent (title), "flow overlay header lower left", GUILayout.ExpandWidth (true));
		rect.x -= 1;
		rect.width += 2;
		Rect rect2 = new Rect (rect.width-10,rect.y+2,25,25);
		
		if (GUI.Button (rect2,"","label") && onAddCallback != null && displayAdd) {
			onAddCallback();
		}
		
		
		if (GUI.Button (rect, title, "flow overlay header lower left")) {
			if(Event.current.button==0){
				EditorPrefs.SetBool (title, !foldOut);	
			}
			if(Event.current.button == 1 && onHeaderClick != null){
				onHeaderClick();
			}
		}
		
		if (displayAdd) {
			GUI.Label (rect2, iconToolbarPlus);
		}
		return foldOut;
	}
	
	private void DoListItems(){
		GUILayout.BeginVertical ((GUIStyle)"PopupCurveSwatchBackground", GUILayout.ExpandWidth (true));
		int swapIndex=-1;

		if (items != null && items.Count > 0) {
			for (int i=0; i< items.Count; i++) {
				GUI.enabled = !(i == selectedIndex);
				GUILayout.BeginHorizontal ();
				
				GUILayout.BeginVertical ();
				GUILayout.Box(GUIContent.none,"PopupCurveSwatchBackground",GUILayout.ExpandWidth(true),GUILayout.Height(1));

				if(drawElementCallback != null){
					drawElementCallback(i);
				}
				
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				
				GUI.enabled = true;
				Rect elementRect = GUILayoutUtility.GetLastRect ();
				switch (Event.current.type) {
				case EventType.MouseDown:
					if (elementRect.Contains (Event.current.mousePosition) && Event.current.button==0) {
						if (onSelectCallback != null) {
							onSelectCallback (i);
						}
						GUI.FocusControl (title + i);
						if(draggable && items.Count>1){
							selectedIndex = i;
							isDragging = true;
						}
					}
					if (elementRect.Contains (Event.current.mousePosition) && Event.current.button==1) {
						GenericMenu genericMenu = new GenericMenu ();
						genericMenu.AddItem(new GUIContent("Remove"),false,RemoveItem,i);
						genericMenu.AddItem(new GUIContent("Move Up"),false,MoveUp,i);
						genericMenu.AddItem(new GUIContent("Move Down"),false,MoveDown,i);
						genericMenu.ShowAsContext();
					}
					break;
				case EventType.MouseUp:
					if (selectedIndex != i && elementRect.Contains (Event.current.mousePosition) && draggable && Event.current.button==0) {
						swapIndex = i;
					}
					isDragging = false;

					break;
				case EventType.MouseDrag:
					if (elementRect.Contains (Event.current.mousePosition) && Event.current.button==0) {
						GUI.FocusControl (title + i);
					}
					break;
				}
			}
		} else {
			GUILayout.Label("List is Empty");
		}
		if (swapIndex != -1) {
			items.Swap (selectedIndex, swapIndex);
			selectedIndex = -2;
		}
		
		if (!isDragging) {
			selectedIndex = -2;
		}
		GUILayout.EndVertical ();
		GUI.enabled = true;
		
	}
	
	private void MoveUp(object index){
		items.Move ((int)index, 1);
	}
	
	
	private void MoveDown(object index){
		items.Move ((int)index, 0);
	}
	
	private void RemoveItem(object index){
		if (onRemoveCallback != null) {
			onRemoveCallback ((int)index);	
		} else {
			items.RemoveAt ((int)index);
		}
	}
	
	public GUIContent iconToolbarMinus = IconContent("Toolbar Minus", "Remove action");
	public GUIContent iconToolbarPlus = IconContent("Toolbar Plus", "Add new action");
	
	public static GUIContent IconContent(string name, string tooltip)
	{
		var t = typeof (EditorGUIUtility);
		var m = t.GetMethod("IconContent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static,
		                    Type.DefaultBinder, new[] {typeof (string)}, null);
		var content = (GUIContent) m.Invoke(t, new[] {name});
		content.tooltip = tooltip;
		return content;
	}
}
