/*
 *  This file is part of TechBoard.
 *  Copyright (C) 2011-2012 Matt Thomson
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *  
 *  For more information on TechBoard, see 
 *  <http://www.matt-thomson.co.uk/software/techboard>.
 */

using System.Windows;
using System.Windows.Controls;

namespace TechBoard.App
{
    public class BlockPropertyEditorSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object xiItem,
                                                    DependencyObject xiContainer)
        {
            DataTemplate template = new DataTemplate();
            PropertyMapping mapping = xiItem as PropertyMapping;

            if (mapping != null)
            {
                // Now get the view attribute from the editor property attribute.
                object[] attrs = mapping.Attribute.GetType().GetCustomAttributes(
                                                    typeof(BlockPropertyEditorAttribute), false);
                BlockPropertyEditorAttribute viewAttr = attrs[0] as BlockPropertyEditorAttribute;
                
                // Set up the template from the view attribute.
                template.VisualTree = new FrameworkElementFactory(viewAttr.ViewType);
            }

            return template;
        }
    }
}
