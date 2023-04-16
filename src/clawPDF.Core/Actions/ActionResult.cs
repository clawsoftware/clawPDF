using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace clawSoft.clawPDF.Core.Actions
{
    public class ActionResult : List<int>
    {
        public ActionResult()
        {
        }

        public ActionResult(int errorCode)
        {
            Add(errorCode);
        }

        public ActionResult(int actionId, int internalErrorCode)
        {
            Add(actionId, internalErrorCode);
        }

        public bool Success => Count == 0;

        public void Add(int actionId, int internalErrorCode)
        {
            var idString = actionId.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
            var errorCodeString = internalErrorCode.ToString(CultureInfo.InvariantCulture).PadLeft(3, '0');
            var errorCode = Convert.ToInt32(idString + errorCodeString);

            Add(errorCode);
        }

        public static implicit operator bool(ActionResult actionResult)
        {
            return actionResult.Success;
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                this.Select(x => x.ToString(CultureInfo.InvariantCulture)).ToArray());
        }
    }

    public class ActionResultDict : Dictionary<string, ActionResult>
    {
        public bool Success
        {
            get { return Values.FirstOrDefault(aR => !aR) == null; }
        }

        public static implicit operator bool(ActionResultDict actionResultDict)
        {
            return actionResultDict.Success;
        }
    }
}