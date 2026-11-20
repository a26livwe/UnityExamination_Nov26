using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
    // Special runtime clip implementation that handles playables that use a scheduling system
    //  such as Audio
    internal class ScheduleRuntimeClip : RuntimeClipBase
    {
        private TimelineClip m_Clip;
        private Playable m_Playable;
        private Playable m_ParentMixer;
        private double m_StartDelay;
        private double m_FinishTail;
        private bool m_Started = false;

        // represents the start point when we want to start getting updated
        public override double start
        {
            get { return Math.Max(0, m_Clip.start - m_StartDelay); }
        }

        public override double duration
        {
            get { return m_Clip.duration + m_FinishTail + m_Clip.start - start; }
        }

        public void SetTime(double time)
        {
            m_Playable.SetTime(time);
        }

        public TimelineClip clip { get { return m_Clip; } }
        public Playable mixer { get { return m_ParentMixer; } }
        public Playable playable { get { return m_Playable; } }

        public ScheduleRuntimeClip(TimelineClip clip, Playable clipPlayable,
                                   Playable parentMixer, double startDelay = 0.2, double finishTail = 0.1)
        {
            Create(clip, clipPlayable, parentMixer, startDelay, finishTail);
        }

        private void Create(TimelineClip clip, Playable clipPlayable, Playable parentMixer,
            double startDe