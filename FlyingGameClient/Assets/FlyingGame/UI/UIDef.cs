using UnityEngine;

namespace Kurisu.UI
{
    public static class UIDef
    {
        #region 公用UI名

        public const string UIMsgBox = "Common/UIMsgBox";

        #endregion

        #region 模块UI名

        public const string UIHomePage = "Home/UIHomePage";

        public const string UIChapterPage = "Chapter/UIChapterPage";

        public const string UIMapItemDetailWindow = "Chapter/UIMapItemDetailWindow";

        public const string UIPveGamePage = "Pve/UIPveGamePage";

        public const string UIPveGamePauseWindow = "Pve/UIPveGamePauseWindow"; 

        public const string UIPveGameWinWindow = "Pve/UIPveGameWinWindow";

        public const string UIPveGameFailWindow = "Pve/UIPveGameFailWindow";

        public const string UISettingPage = "Setting/UISettingPage";

        public const string UIGameExitWindow = "AppMain/UIGameExitWindow";

        public const string UIHelpPage = "Help/UIHelpPage";
        #endregion

        /// <summary>
        /// 按钮被选中时的颜色
        /// </summary>
        public readonly static Color SelectedColorForBtn = new Color(240f / 255f, 240f / 255f, 42f / 255f, 255f / 255f);
    }
}

