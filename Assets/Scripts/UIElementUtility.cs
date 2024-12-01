using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public static class UIElementUtility
{
    [Flags]
    public enum SideType
    {
        Left    = 1 << 0,
        Right   = 1 << 1,
        Top     = 1 << 2,
        Bottom  = 1 << 3,

        All         = Left | Right | Top | Bottom,
        Horizontal  = Left | Right,
        Vertical    = Top | Bottom,

        TopLeft     = Top | Left,
        TopRight    = Top | Right,
        BottomLeft  = Bottom | Left,
        BottomRight = Bottom | Right,

        NoLeft      = All ^ Left,
        NoRight     = All ^ Right,
        NoTop       = All ^ Top,
        NoBottom    = All ^ Bottom
    }

    [Flags]
    public enum CornerType
    {
        TopLeft     = 1 << 0,
        TopRight    = 1 << 1,
        BottomLeft  = 1 << 2,
        BottomRight = 1 << 3,

        All     = TopLeft | TopRight | BottomLeft | BottomRight,
        TLBR    = TopLeft | BottomRight,
        TRBL    = TopRight | BottomLeft,

        Top     = TopLeft | TopRight,
        Bottom  = BottomLeft | BottomRight,
        Left    = TopLeft | BottomLeft,
        Right   = TopRight | BottomRight,

        NoTopLeft       = All ^ TopLeft,
        NoTopRight      = All ^ TopRight,
        NoBottomLeft    = All ^ BottomLeft,
        NoBottomRight   = All ^ BottomRight
    }

    public static void SetPadding(this VisualElement element, float padding, SideType sideType = SideType.All)
    {
        if (sideType.HasFlag(SideType.Left))
            element.style.paddingLeft = padding;
        if (sideType.HasFlag(SideType.Right))
            element.style.paddingRight = padding;
        if (sideType.HasFlag(SideType.Top))
            element.style.paddingTop = padding;
        if (sideType.HasFlag(SideType.Bottom))
            element.style.paddingBottom = padding;
    }

    public static void SetPadding(this VisualElement element, float left, float right, float top, float bottom)
    {
        element.style.paddingLeft = left;
        element.style.paddingRight = right;
        element.style.paddingTop = top;
        element.style.paddingBottom = bottom;
    }

    public static void SetHorizontalPadding(this VisualElement element, float padding) => SetPadding(element, padding, SideType.Horizontal);

    public static void SetVerticalPadding(this VisualElement element, float padding) => SetPadding(element, padding, SideType.Vertical);

    public static void SetBorder(this VisualElement element, float thickness, Color color = default, SideType sideType = SideType.All)
    {

        if (sideType.HasFlag(SideType.Left))
        {
            element.style.borderLeftWidth = thickness;
            element.style.borderLeftColor = color;
        }
        if (sideType.HasFlag(SideType.Right))
        {
            element.style.borderRightWidth = thickness;
            element.style.borderRightColor = color;
        }
        if (sideType.HasFlag(SideType.Top))
        {
            element.style.borderTopWidth = thickness;
            element.style.borderTopColor = color;
        }
        if (sideType.HasFlag(SideType.Bottom))
        {
            element.style.borderBottomWidth = thickness;
            element.style.borderBottomColor = color;
        }
    }

    public static void SetHorizontalBorder(this VisualElement element, float thickness, Color color = default) => SetBorder(element, thickness, color, SideType.Horizontal);

    public static void SetVerticalBorder(this VisualElement element, float thickness, Color color = default) => SetBorder(element, thickness, color, SideType.Vertical);

    public static void SetMargin(this VisualElement element, float margin, SideType sideType = SideType.All)
    {
        if (sideType.HasFlag(SideType.Left))
            element.style.marginLeft = margin;
        if (sideType.HasFlag(SideType.Right))
            element.style.marginRight = margin;
        if (sideType.HasFlag(SideType.Top))
            element.style.marginTop = margin;
        if (sideType.HasFlag(SideType.Bottom))
            element.style.marginBottom = margin;
    }

    public static void SetMargin(this VisualElement element, float left, float right, float top, float bottom)
    {
        element.style.marginLeft = left;
        element.style.marginRight = right;
        element.style.marginTop = top;
        element.style.marginBottom = bottom;
    }

    public static void SetHorizontalMargin(this VisualElement element, float margin) => SetMargin(element, margin, SideType.Horizontal);

    public static void SetVerticalMargin(this VisualElement element, float margin) => SetMargin(element, margin, SideType.Vertical);

    public static void SetCorner(this VisualElement element, float radius, CornerType cornerType = CornerType.All)
    {
        if (cornerType.HasFlag(CornerType.TopLeft))
            element.style.borderTopLeftRadius = radius;
        if (cornerType.HasFlag(CornerType.TopRight))
            element.style.borderTopRightRadius = radius;
        if (cornerType.HasFlag(CornerType.BottomLeft))
            element.style.borderBottomLeftRadius = radius;
        if (cornerType.HasFlag(CornerType.BottomRight))
            element.style.borderBottomRightRadius = radius;
    }

}
