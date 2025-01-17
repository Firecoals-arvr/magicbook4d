//-------------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2019 Tasharen Entertainment Inc
//-------------------------------------------------

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Very simple sprite anim. Attach to a sprite and specify a common prefix such as "idle" and it will cycle through them.
/// </summary>

[ExecuteInEditMode]
[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/UI/Sprite Animation")]
public class UISpriteAnimation : MonoBehaviour
{
	/// <summary>
	/// Index of the current frame in the sprite anim.
	/// </summary>

	public int frameIndex = 0;

	[HideInInspector][SerializeField] protected int mFPS = 30;
	[HideInInspector][SerializeField] protected string mPrefix = "";
	[HideInInspector][SerializeField] protected bool mLoop = true;
	[HideInInspector][SerializeField] protected bool mSnap = true;

	protected UISprite mSprite;
	protected float mDelta = 0f;
	protected bool mActive = true;
	protected List<string> mSpriteNames = new List<string>();

	/// <summary>
	/// Number of frames in the anim.
	/// </summary>

	public int frames { get { return mSpriteNames.Count; } }

	/// <summary>
	/// Animation framerate.
	/// </summary>

	public int framesPerSecond { get { return mFPS; } set { mFPS = value; } }

	/// <summary>
	/// Set the name prefix used to filter sprites from the atlas.
	/// </summary>

	public string namePrefix { get { return mPrefix; } set { if (mPrefix != value) { mPrefix = value; RebuildSpriteList(); } } }

	/// <summary>
	/// Set the anim to be looping or not
	/// </summary>

	public bool loop { get { return mLoop; } set { mLoop = value; } }

	/// <summary>
	/// Returns is the anim is still playing or not
	/// </summary>

	public bool isPlaying { get { return mActive; } }

	/// <summary>
	/// Rebuild the sprite list first thing.
	/// </summary>

	protected virtual void Start () { RebuildSpriteList(); }

	/// <summary>
	/// Advance the sprite anim process.
	/// </summary>

	protected virtual void Update ()
	{
		if (mActive && mSpriteNames.Count > 1 && Application.isPlaying && mFPS > 0)
		{
			mDelta += Mathf.Min(1f, RealTime.deltaTime);
			float rate = 1f / mFPS;

			while (rate < mDelta)
			{
				mDelta = (rate > 0f) ? mDelta - rate : 0f;

				if (++frameIndex >= mSpriteNames.Count)
				{
					frameIndex = 0;
					mActive = mLoop;
				}

				if (mActive)
				{
					mSprite.spriteName = mSpriteNames[frameIndex];
					if (mSnap) mSprite.MakePixelPerfect();
				}
			}
		}
	}

	/// <summary>
	/// Rebuild the sprite list after changing the sprite name.
	/// </summary>

	public void RebuildSpriteList ()
	{
		if (mSprite == null) mSprite = GetComponent<UISprite>();
		mSpriteNames.Clear();

		if (mSprite != null)
		{
			var atlas = mSprite.atlas;

			if (atlas != null)
			{
				var sprites = atlas.spriteList;

				for (int i = 0, imax = sprites.Count; i < imax; ++i)
				{
					var sprite = sprites[i];
					if (string.IsNullOrEmpty(mPrefix) || sprite.name.StartsWith(mPrefix)) mSpriteNames.Add(sprite.name);
				}
				mSpriteNames.Sort();
			}
		}
	}

	/// <summary>
	/// Reset the anim to the beginning.
	/// </summary>

	public void Play () { mActive = true; }

	/// <summary>
	/// Pause the anim.
	/// </summary>

	public void Pause () { mActive = false; }

	/// <summary>
	/// Reset the anim to frame 0 and activate it.
	/// </summary>

	public void ResetToBeginning ()
	{
		mActive = true;
		frameIndex = 0;

		if (mSprite != null && mSpriteNames.Count > 0)
		{
			mSprite.spriteName = mSpriteNames[frameIndex];
			if (mSnap) mSprite.MakePixelPerfect();
		}
	}
}
