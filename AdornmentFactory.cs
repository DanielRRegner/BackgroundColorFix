using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Editor;

namespace BackgroundColorFix
{
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("text")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    sealed class BackgroundColorAdornmentFactory : IWpfTextViewCreationListener
    {
        [Export(typeof(AdornmentLayerDefinition))]
        [Name("BackgroundColorFix")]
        [Order(Before = PredefinedAdornmentLayers.Selection)]
        [TextViewRole(PredefinedTextViewRoles.Document)]
        AdornmentLayerDefinition editorAdornmentLayer = null;

        [Import]
        IViewTagAggregatorFactoryService AggregatorService = null;

        [Import]
        IClassificationFormatMapService FormatMapService = null;

        // For getting colorable items
        [Import]
        IVsFontsAndColorsInformationService FCService = null;

        [Import]
        IVsEditorAdaptersFactoryService AdaptersService = null;

        public void TextViewCreated(IWpfTextView textView)
        {
            new BackgroundColorVisualManager(textView, 
                                             AggregatorService.CreateTagAggregator<ClassificationTag>(textView),
                                             FormatMapService.GetClassificationFormatMap(textView),
                                             FCService, AdaptersService);
        }
    }
}
