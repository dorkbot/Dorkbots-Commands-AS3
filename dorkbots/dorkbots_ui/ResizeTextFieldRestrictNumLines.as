package dorkbots.dorkbots_ui
{
	import flash.text.TextField;

	public class ResizeTextFieldRestrictNumLines
	{
		public function ResizeTextFieldRestrictNumLines()
		{
			
		}
		
		public static function resizeToMaxLines(textField:TextField, maxWidth:Number, maxNumLines:uint):void
		{
			var continueResize:Boolean = true;
			
			while(continueResize)
			{
				if(textField.numLines > maxNumLines && textField.width < maxWidth)
				{
					textField.width += 10;
				}
				else
				{
					if (textField.width > maxWidth)
					{
						textField.width = maxWidth;
					}
					
					continueResize = false;
					
					break;
				}
			}
		}
	}
}