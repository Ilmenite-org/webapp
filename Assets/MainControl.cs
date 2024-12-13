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
        private Slider slider;

        private Label temp_label;
        private long temp_value = 0;
        private bool temp_sliding_time = false;
        private DateTime temp_last_update;

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

            slider = root.Q<Slider>("InfiniteScrollbar");
            slider.RegisterCallback<MouseCaptureOutEvent>(ResetSlider);
            slider.RegisterValueChangedCallback(ApplyScrollTime);

            temp_label = root.Q<Label>("Temp");
        }

        private void ApplyScrollTime(ChangeEvent<float> e)
        {
            if (e.newValue == 6f)
            {
                temp_sliding_time = false;
                CancelInvoke();
            }
            else if (!temp_sliding_time)
            {
                temp_sliding_time = true;
                temp_last_update = DateTime.Now;
                InvokeRepeating("MoveTimeView", 0f, 1f / 60f);
            }
        }

        private void MoveTimeView()
        {
            var x = slider.value;
            var interval = 16f / Mathf.Abs(x - 6f) - Mathf.Floor(Mathf.Abs(x - 6f) - (x / 100000f));   // 16ms = ~60Hz
            var now = DateTime.Now;
            if (temp_last_update.AddMilliseconds(interval) < now)
            {
                if (x == 6f)
                {
                    temp_last_update = now;
                    return;
                }

                var k = Math.Pow(1000d, Math.Floor(Math.Abs(x - 6d))) * ((x - 6d) / Math.Abs(x - 6d));  // multiplier 1/1,000/1,000,000/1,000,000,000/10^12/10^15
                temp_value += (long)k;

                temp_label.text = temp_value.ToString();
                temp_last_update = now;
            }
        }

        private void ResetSlider(MouseCaptureOutEvent e)
        {
            CancelInvoke();
            slider.value = 6f;
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
