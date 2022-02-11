# Features #
- Shrink PDF
- Image to PDF
- PDF to PNG/JPG (300/600dpi)

# Instructions #
1. Find the latest release under "releases"
2. Download .msi to install.
<br/>.exe file still requires .msi file to install

    Note: To uninstall, simply go to "Add or remove programs"

## NuGet Libraries ##
- Uses `PDFSharp` to export PDF
- Uses `PDFiumViewer` to export images

## PDFiumViewer installation issue fix ##
1. Install `PdfiumViewer` NuGet package
2. Install `PdfiumViewer.Native.x86.v8-xfa` NuGet package
3. Install `PdfiumViewer.Native.x86_64.v8-xfa` NuGet package
