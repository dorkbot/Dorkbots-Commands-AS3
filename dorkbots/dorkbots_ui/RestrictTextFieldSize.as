package dorkbots.dorkbots_ui
{
	import flash.text.TextField;
	import flash.text.TextFieldAutoSize;

	public class RestrictTextFieldSize
	{
		public function RestrictTextFieldSize()
		{
		}
		
		public static function restrictWidth(textField:TextField, minWidth:Number, maxWidth:Number, buffer = 10, autoSize:String = TextFieldAutoSize.LEFT):void
		{
			// first turn off multiline and word wrap
			textField.multiline = false;
			textField.wordWrap = false;
			
			var textWidth:Number = textField.textWidth + buffer;
			textWidth = Math.max(textWidth, minWidth);
			textWidth = Math.min(textWidth, maxWidth);
			
			// turn them back on
			textField.multiline = true;
			textField.wordWrap = true;
			
			// turn auto size on 
			textField.autoSize = TextFieldAutoSize.LEFT;
			
			// resize
			textField.width = textWidth;
			textField.height = textField.textHeight + buffer;
		}
	}
}