using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FakeVimCharOnlyMinimal : MonoBehaviour
{
    public RectTransform textViewport;
    public RectTransform cursor;
    public TMP_Text textLayer;

    public float gutterWidth = 64f;
    public float paddingLeft = 8f;
    public float paddingTop  = 0f;
    public float lineHeight  = 24f;
    public float charAdvance = 11f;

    public TextAsset initialTextAsset;
    [TextArea(6,12)] public string initialTextFallback;

    List<string> lines = new List<string>();
    int row = 0, col = 0;
    string reg = "";
    string _sceneAuthoredText = "";

    void Awake()
    {
        _sceneAuthoredText = textLayer ? textLayer.text : "";
    }

    void Start()
    {
        // Reset to original each Play
        string src =
            (initialTextAsset && !string.IsNullOrEmpty(initialTextAsset.text)) ? initialTextAsset.text :
            (!string.IsNullOrEmpty(_sceneAuthoredText)) ? _sceneAuthoredText :
            initialTextFallback;

        SetWorkingText(src);
        row = 0; col = 0;
        ClampRowCol();
        UpdateCursor();
    }

    void Update()
    {
        if (!Application.isFocused) return;

        // Movement: h j k l
        if (Down(KeyCode.H)) { col = Mathf.Max(0, col - 1); UpdateCursor(); }
        if (Down(KeyCode.L)) { col = Mathf.Min(LineLen(row), col + 1); UpdateCursor(); }
        if (Down(KeyCode.K)) { row = Mathf.Max(0, row - 1); col = Mathf.Min(col, LineLen(row)); UpdateCursor(); }
        if (Down(KeyCode.J)) { row = Mathf.Min(lines.Count - 1, row + 1); col = Mathf.Min(col, LineLen(row)); UpdateCursor(); }

        // Copy char under cursor: y
        if (Down(KeyCode.Y))
        {
            reg = GetCharUnderCursor();
        }

        // Paste char at cursor: p (inserts then moves right)
        if (Down(KeyCode.P) && !string.IsNullOrEmpty(reg))
        {
            string s = lines[row];
            col = Mathf.Clamp(col, 0, s.Length);
            lines[row] = s.Insert(col, reg);
            col += reg.Length;
            ApplyLinesToText();
            UpdateCursor();
        }
    }

    void SetWorkingText(string src)
    {
        lines.Clear();
        foreach (var s in (src ?? "").Replace("\r", "").Split('\n'))
            lines.Add(s);
        if (lines.Count == 0) lines.Add("");
        ApplyLinesToText();
    }

    void ApplyLinesToText() => textLayer.text = string.Join("\n", lines);

    string GetCharUnderCursor()
    {
        var s = lines[row];
        if (s.Length == 0) return "";
        int c = Mathf.Clamp(col, 0, s.Length - 1);
        return s[c].ToString();
    }

    int LineLen(int r) => (r >= 0 && r < lines.Count) ? lines[r].Length : 0;

    void ClampRowCol()
    {
        row = Mathf.Clamp(row, 0, Mathf.Max(0, lines.Count - 1));
        col = Mathf.Clamp(col, 0, LineLen(row));
    }

    void UpdateCursor()
    {
        if (!cursor) return;
        float x = gutterWidth + paddingLeft + col * charAdvance;
        float y = -(paddingTop + row * lineHeight);
        cursor.anchoredPosition = new Vector2(x, y);
    }

    static bool Down(KeyCode k) => Input.GetKeyDown(k);
}

