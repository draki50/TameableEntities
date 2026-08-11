using UnityEngine;
using Verse;

namespace TameableAnomalies.Dialogs
{
    public class Dialog_RenameEntity : Window
    {
        private readonly Pawn pawn;
        private string curName;
        private bool focused;
        public Dialog_RenameEntity(Pawn pawn)
        {
            this.pawn = pawn;
            curName = pawn.Name?.ToStringShort ?? pawn.LabelShort;

            forcePause = true;
            absorbInputAroundWindow = true;
            closeOnClickedOutside = true;
        }

        public override Vector2 InitialSize => new Vector2(420f, 160f);

        public override void DoWindowContents(Rect inRect)
        {
            Widgets.Label(new Rect(0f, 0f, 300f, 30f), "Rename Entity.");

            GUI.SetNextControlName("RenameField");

            curName = Widgets.TextField(
                new Rect(0f, 35f, 300f, 35f),
                curName);

            if (!focused)
            {
                GUI.FocusControl("RenameField");
                focused = true;
            }
            if (Widgets.ButtonText(new Rect(20f, 85f, 150f, 35f), "Accept"))
            {
                pawn.Name = new NameSingle(curName);
                Close();
            }

            if (Widgets.ButtonText(new Rect(210f, 85f, 150f, 35f), "Cancel"))
            {
                Close();
            }

        }
    }
}