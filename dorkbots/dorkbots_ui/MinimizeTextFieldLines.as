package dorkbots.dorkbots_ui
{
	import flash.text.TextField;
	import flash.text.TextFieldAutoSize;

	public class MinimizeTextFieldLines
	{
		public function MinimizeTextFieldLines(pvt:PrivateClass)
		{
			
		}
		
		public static function minimize(textField:TextField, lines:uint, truncate:Boolean = false, buffer:uint = 4):uint
		{
			var minimize:MinimizeTextFieldLines = new MinimizeTextFieldLines(new PrivateClass());
			return minimize.minimize(textField, lines, truncate, buffer);
		}
		
		public function minimize(textField:TextField, lines:uint, truncate:Boolean = false, buffer:uint = 4):uint
		{
			var characterRemoveCnt:uint = 0;
			var textString:String = "";
			var numLinesTooMany:Boolean = false;
			
			while (textField.numLines > lines)
			{
				numLinesTooMany = true;
				textString = textField.text;
				textField.text = textString.slice(0, -1);
				characterRemoveCnt++;
			}
			
			if (truncate && numLinesTooMany) 
			{
				textField.height = textField.textHeight + buffer;
				textField.autoSize = TextFieldAutoSize.NONE;
				// adds text to the end to trigger the truncating code
				textField.appendText("&&");
				characterRemoveCnt += TruncateTextField.truncateText(textField, true, lines);
				textField.height = textField.textHeight + buffer;
				// remove two counts for adding "&&"
				if (characterRemoveCnt >= 2) characterRemoveCnt -= 2;
			}
			// this is needed for situations where the calling class might have a dynamic lines argument. Otherwise it would be simpler to just use TruncateTextField
			else if (truncate && lines == 1)
			{
				characterRemoveCnt += TruncateTextField.truncateText(textField, true, lines);
				textField.height = textField.textHeight + buffer;
			}
			
			return characterRemoveCnt;
		}
	}
}

// this Private Class is only used to "jam" the param in the singleton constructor.  So only this singleton can instiate itself
class PrivateClass
{
	public function PrivateClass()
	{
	}
}