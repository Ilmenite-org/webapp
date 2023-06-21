using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace Ilmenite
{
    public class MainControl : MonoBehaviour
    {
        private List<Event> Events = new List<Event>();
        private ScrollView svTimeline;

        private const string INFINITE_SCROLLER = "InfiniteScroller";

        private void OnEnable()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            var btnNew = root.Q<Button>("NewTimeline");
            var btnOpen = root.Q<Button>("OpenTimeline");
            var btnSave = root.Q<Button>("SaveTimeline");

            var eventGroup = root.Q("Event");
            var btnAddEvent = eventGroup.Q<Button>("Add");
            btnAddEvent.clicked += () => AddEvent();
            var btnEditEvent = eventGroup.Q<Button>("Edit");
            btnEditEvent.clicked += () => EditEvent();
            var btnDeleteEvent = eventGroup.Q<Button>("Delete");
            btnDeleteEvent.clicked += () => DeleteEvent();

            svTimeline = root.Q<ScrollView>("TimelineScrollView");

            var infiniteScroller = root.Q<Scroller>(INFINITE_SCROLLER);
            infiniteScroller.lowValue = 0f;
            infiniteScroller.highValue = 1f;
            infiniteScroller.value = .5f;
            infiniteScroller.RegisterCallback<MouseUpEvent>(ResetSlider);
            infiniteScroller.Q("unity-tracker").pickingMode = PickingMode.Ignore;
            infiniteScroller.Q("unity-tracker").SetEnabled(false);
        }

        private void ResetSlider(MouseUpEvent e)
        {
            svTimeline.Q<Scroller>(INFINITE_SCROLLER).value = .5f;
        }

        private void AddEvent()
        {
        }

        private void EditEvent()
        {
        }

        private void DeleteEvent() { }
    }
}
