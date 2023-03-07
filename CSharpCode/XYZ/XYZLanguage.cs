using System;
using System.Collections.Generic;
using System.Text;


// Translation from english is not yet complete. Most hint texts are not translated

namespace XYZ
{
    class XYZLanguage
    {

        public static void Translate()
        {

            switch (XYZMainForm.TAAL)
            {
                case 1:
                    
                    XYZMainForm.StrCancel = "Cancel";
                    XYZMainForm.StrFile = "Bestand";
                    
                    XYZMainForm.StrOpenFile = "Open HPGL bestand";
                    XYZMainForm.StrOpenGCODEFile="Open *.gcode bestand";
                    XYZMainForm.StrOpenDrillFile = "Open een boor (*.drl) bestand";
                    XYZMainForm.StrSaveFile = "Bewaar als HPGL bestand";
                    XYZMainForm.StrLoadModel = "Model afbeelding laden";
                    XYZMainForm.StrClearModel = "Model afbeelding wissen";
                    XYZMainForm.StrClearListbox = "Listbox wissen";
                    XYZMainForm.StrClearDrawing = "Tekening wissen";
                    XYZMainForm.StrClearAll = "Alles wissen";

                    XYZMainForm.StrMirror = "Spiegel Y";
                    XYZMainForm.StrDrawing = "Tekenen";
                    XYZMainForm.StrUndo = "Ongedaan maken";
                    XYZMainForm.StrUndoSteps = "Aantal stappen";
                    XYZMainForm.StrColor = "Kleur";
                    XYZMainForm.StrThickness = "Pen dikte";

                    XYZMainForm.StrFoot = "Eerst bovenaan de checkbox 'Tekenen' aankruisen.   Door de linker muistoets in te drukken kan u vrij op het scherm tekenen";
                    
                    XYZMainForm.StrDraw = "Tekenen";
                    XYZMainForm.StrCommand = "Aansturen"; // Command mode

                    XYZMainForm.StrMenu = "Menu";
                    XYZMainForm.StrDelay = "vertragen";
                    XYZMainForm.StrReset = "Reset";
                    XYZMainForm.StrZSteps = "Z stappen";
                    XYZMainForm.StrUp = "omhoog";
                    XYZMainForm.StrDown = "omlaag";
                    

                    XYZMainForm.StrCut = "Cutter";
                    XYZMainForm.StrDrill = "Boren";
                    XYZMainForm.StrScreen = "Scherm";
                    XYZMainForm.StrView = "Bekijk";
                    XYZMainForm.StrPrint = "Print";
                    XYZMainForm.StrDelete = "Verwijder";
                    XYZMainForm.StrQuit = "Stoppen";
                    XYZMainForm.StrHelp = "Help";
                    XYZMainForm.StrSave = "Bewaar";
                    XYZMainForm.StrSaveNew = "Bewaar als nieuw";
                    XYZMainForm.StrSetup = "Setup";


                    XYZMainForm.StrStepperResolution = "Resolutie XY stappen motors";
                    XYZMainForm.StrColorButton = "Kleur knoppen";
                    XYZMainForm.StrColorForm = "Formkleur";
                    XYZMainForm.StrColorFond = "Fondkleur";
                    
                    XYZMainForm.StrManual = "Manual";
                    XYZMainForm.StrOnlineManual = "Online manual";
                    XYZMainForm.StrContactUs = "Contact us";
                    XYZMainForm.StrVersion = "Version 1.1  31/01/2018";
                    XYZMainForm.StrAutor = "Auteur";
                    XYZMainForm.StrStart = "Start";
                    XYZMainForm.StrIconColor = "Icon kleur";       // = also for button color
                    XYZMainForm.StrBackColor = "Achtergrond kleur";
                    XYZMainForm.StrComport = "Comport";
                    XYZMainForm.StrClear = "Wissen";
                    XYZMainForm.StrPrevious = "Vorige";
                   
                    XYZMainForm.StrDefault = "Default";
                    XYZMainForm.StrNoSerialPorts = "Geen poorten gevonden";
                    XYZMainForm.StrNotFound = "niet gevonden";
                    XYZMainForm.StrAttention = "Aandacht";
                    XYZMainForm.StrSelection = "Selectie";
                    XYZMainForm.StrSaveAndQuit = "Bewaren";

                    XYZMainForm.StrLaunch = "Starten";

                    XYZMainForm.StrStartPlot = "Start plotter";
                    XYZMainForm.StrStartCut = "Start cutter";
                    XYZMainForm.StrStartDrill = "Start boren";
                    XYZMainForm.StrStartDispense = "Start dispenser";
                    XYZMainForm.StrStartMill = "Start milling";

                    XYZMainForm.StrReopen = "Heropen";
                    XYZMainForm.StrPause = "Pause";
                    XYZMainForm.StrDimension = "Afmetingen";

                    XYZMainForm.StrImport = "Importeren van";
                    

                    XYZMainForm.StrBT = "Selectie van het correcte uitgaande Bluetooth comportnummer.\n" +
                          "Voer het comportnummer niet zelf in maar klik op het naar beneden\n" +
                          "wijzende ^  teken en selecteer het correcte nummer in de lijst (dropdown list).\n" +
                          "\n" +
                        "Indien er geen comportnummer in de dropdownlist staat, verifieer\n" +
                        "dat het apparaat gepaard is en AAN staat.\n" +
                       "\n" +
                       "Indien het correcte nummer nog niet gekend is:\n" +
                      "'Select show Bluetooth devices' -> 'More bluetooth options'\n" +
                      "om het uitgaande comportnummer Comx te zien\n" +
                      "Selecteer deze COMx in de dropdown list.\n";

                    XYZMainForm.StrUSB = "Voer aub het comportnummer niet zelf in maar klik op het naar beneden\n" +
                           " wijzende ^  teken en selecteer het correcte nummer in de lijst.(dropdown list)\n" +
                           "\n" +
                           "Indien er geen comportnummer in de dropdownlist staat, zijn de mogelijkheden:\n" +
                           "- het apparaat is niet verbonden met een USB poort.\n" +
                           "- de USB serial drivers zijn niet geinstalleerd.\n" +
                            "- de USB omzetter is defect.\n" +
                           "\n" +
                          "Indien u het correcte comportnumber nog niet weet,\n" +
                          "klik dan tweemaal op het naar beneden wijzende  ^  teken:\n" +
                          "- eenmaal wanneer het apparaat WEL verbonden is met USB.\n" +
                          "- eenmaal wanneer het apparaat NIET verbonden is met USB.\n" +
                          "De COMx die verschijnt wanneer het apparaat verbonden is," +
                          "is de correcte keuze. Selecteer deze COMx in de dropdown list.\n" +
                                "\n" +
                         "Een eerste installatie kan verschillende minuten duren, ook op een snelle PC.\n";


                    XYZMainForm.StrHPGLInputProperties = "HPGL input eigenschappen";
                    XYZMainForm.StrScreenCoordinates = "Scherm coordinaten, getekend met dit programma";
                    XYZMainForm.StrIndependent = "Plot afmetingen hangen niet af van de papier afmetingen";
                    XYZMainForm.StrDependent = "Plot afmetingen hangen af van de papier afmetingen";
                    XYZMainForm.StrDoNotScale = "Plotten zonder schaal (PCB)";
                    XYZMainForm.StrDoScale = "Plotten volgens afmeting van papier of vinyl";
                    XYZMainForm.StrPrecision = "Precisie";

                    XYZMainForm.StrScrCoord = "Scherm coordinaten";
                    XYZMainForm.StrSetOther = "Ander formaat input of output";
                    XYZMainForm.StrScaled = "Plot op schaal";
                                        
                    XYZMainForm.StrSelectPad = "Selecteer een pad:";
                    XYZMainForm.StrSetProperties = "Eigenschappen instellen:";
                    XYZMainForm.StrDotDistance = "Dot afstand";
                    XYZMainForm.StrDotExtrusion = "Dot extrusie";
                    XYZMainForm.StrSet = "Set";
                    XYZMainForm.StrFileContent = "Inhoud bestand:";

                    XYZMainForm.StrSettings = "Instellingen";
                    XYZMainForm.StrLengthShaftX = "Lengte X bar";
                    XYZMainForm.StrLengthShaftY = "Lengte Y bar";
                    XYZMainForm.StrMargeX = "Linker marge X mm";
                    XYZMainForm.StrMargeY = "Top marge Y mm";

                    break;
                case 2:
                    XYZMainForm.StrFile = "File";
                    
                    XYZMainForm.StrOpenFile = "Öffnen Sie die HPGL-Datei";
                    XYZMainForm.StrOpenGCODEFile = "Öffnen Sie die *.gcode Datei";
                    XYZMainForm.StrOpenDrillFile = "Öffnen Sie die bohren-Datei (*.drl)";
                    XYZMainForm.StrSaveFile = "HPGL-Datei speichern";
                    XYZMainForm.StrLoadModel = "Modellbild laden";
                    XYZMainForm.StrClearModel = "Modellbild löschen";
                    XYZMainForm.StrClearListbox = "Löschen der Listbox";
                    XYZMainForm.StrClearDrawing = "Zeichnung löschen";
                    XYZMainForm.StrClearAll = "alles löschen";

                    XYZMainForm.StrMirror = "Y gespiegelt";
                    XYZMainForm.StrDrawing = "Zeichnen";
                    XYZMainForm.StrUndo = "zurück";
                    XYZMainForm.StrUndoSteps = "Tritte zurück";
                    XYZMainForm.StrColor = "Farbe";
                    XYZMainForm.StrThickness = "Linienstärke";

                    XYZMainForm.StrFoot = "Aktivieren Sie das Kontrollkästchen 'Zeichnen'.   Zeichnen Sie freihändig, während die linke Maustaste gedrückt wird.";

                    XYZMainForm.StrDraw = "Design";
                    XYZMainForm.StrCommand = "Kommando";

                    XYZMainForm.StrMenu = "Menu";
                    XYZMainForm.StrDelay = "verlangsamen";
                    XYZMainForm.StrReset = "Zurücksetzen";
                    XYZMainForm.StrZSteps = "Z Schritte";
                    XYZMainForm.StrUp = "nach oben";
                    XYZMainForm.StrDown = "absteigen";

                    XYZMainForm.StrCut = "Schnitt";
                    XYZMainForm.StrDrill = "Bohren";
                    XYZMainForm.StrScreen = "Bildschirm";
                    XYZMainForm.StrView = "Ansicht";
                    XYZMainForm.StrPrint = "Afdrucken";
                    XYZMainForm.StrDelete = "Delete";
                    XYZMainForm.StrQuit = "Beenden";
                    XYZMainForm.StrHelp = "Hilfe";
                    XYZMainForm.StrSave = "Speichern";
                    XYZMainForm.StrSaveNew = "Speichern as neues";
                    XYZMainForm.StrSetup = "Konfiguration";
                    
                    XYZMainForm.StrStepperResolution = "Schrittmotorauflösung";
                    XYZMainForm.StrColorButton = "Knopffarbe";
                    XYZMainForm.StrColorForm = "Formfarbe";
                    XYZMainForm.StrColorFond = "Fondfarbe";

                    XYZMainForm.StrManual = "Manual";
                    XYZMainForm.StrOnlineManual = "Online manual";
                    XYZMainForm.StrContactUs = "Contact us";
                    XYZMainForm.StrVersion = "Version 1.1  31/01/2018";
                    XYZMainForm.StrAutor = "Auteur";
                    XYZMainForm.StrStart = "Start";
                    XYZMainForm.StrIconColor = "Icon color";       // =  button color
                    XYZMainForm.StrBackColor = "Backcolor";
                    XYZMainForm.StrComport = "Comport";
                    XYZMainForm.StrClear = "Clear";
                    XYZMainForm.StrPrevious = "Previous";
                    XYZMainForm.StrCancel = "Abbrechen";
                    XYZMainForm.StrDefault = "Default";
                    XYZMainForm.StrNoSerialPorts = "No serial port founds";
                    XYZMainForm.StrNotFound = "not found";
                    XYZMainForm.StrAttention = "Attention";
                    XYZMainForm.StrSelection = "Selection";
                    XYZMainForm.StrSaveAndQuit = "Speichern";
                    XYZMainForm.StrLaunch = "Starten";
                    XYZMainForm.StrImport = "Importieren von";

                    XYZMainForm.StrStartPlot = "Starten";                   
                    XYZMainForm.StrStartCut = "Starten Cutter";
                    XYZMainForm.StrStartDrill = "Starten bohren"; // drill ?
                    XYZMainForm.StrStartDispense = "Starten Dispenser";
                    XYZMainForm.StrStartMill = "Starten Mill";
                    XYZMainForm.StrReopen = "Wieder öffnen";
                    XYZMainForm.StrPause = "Pause";
                    XYZMainForm.StrDimension = "Abmessungen";
                                      

                    XYZMainForm.StrUSB = "Voer aub het comportnummer niet zelf in maar klik op het naar beneden\n" +
                           " wijzende ^  teken en selecteer het correcte nummer in de lijst.(dropdown list)\n" +
                           "\n" +
                           "Indien er geen comportnummer in de dropdownlist staat, zijn de mogelijkheden:\n" +
                           "- het apparaat is niet verbonden met een USB poort.\n" +
                           "- de USB serial drivers zijn niet geinstalleerd.\n" +
                            "- de USB omzetter is defect.\n" +
                           "\n" +
                          "Indien u het correcte comportnumber nog niet weet,\n" +
                          "klik dan tweemaal op het naar beneden wijzende  ^  teken:\n" +
                          "- eenmaal wanneer het apparaat WEL verbonden is met USB.\n" +
                          "- eenmaal wanneer het apparaat NIET verbonden is met USB.\n" +
                          "De COMx die verschijnt wanneer het apparaat verbonden is," +
                          "is de correcte keuze. Selecteer deze COMx in de dropdown list.\n" +
                                "\n" +
                          "Een eerste installatie kan verschillende minuten duren, zelfs op een snelle PC.\n";
                    

                    XYZMainForm.StrHPGLInputProperties = "HPGL Eigenschaften";
                    XYZMainForm.StrScreenCoordinates = "Bildschirmkoordinaten, zeichned mit diesem Programm";
                    XYZMainForm.StrIndependent = "Die Plotdimensionen sind nicht von der Papiergröße abhängig";
                    XYZMainForm.StrDependent = "Die Plotdimensionen sind abhängig von der Papiergröße.";
                    XYZMainForm.StrDoNotScale = "Nicht skalieren zur Ausgabe (PCB)";
                    XYZMainForm.StrDoScale = "Skalieren, entsprechend der Größe des Papiers/Vinyl";
                    XYZMainForm.StrPrecision = "Präzision";
                                       

                    XYZMainForm.StrScrCoord = "Bildschermkoordinaten";
                    XYZMainForm.StrSetOther = "anderes Eingabe- oder Ausgabeformat";
                    XYZMainForm.StrScaled = "Plot skalieren";

                    XYZMainForm.StrSelectPad = "Pfad Auswahl:";
                    XYZMainForm.StrSetProperties = "Pfad Eigenschaften:";
                    XYZMainForm.StrDotDistance = "Punktabstand";
                    XYZMainForm.StrDotExtrusion = "Extrusion";
                    XYZMainForm.StrSet = "einstellen";
                    XYZMainForm.StrFileContent = "Dateiinhalt:";

                    XYZMainForm.StrSettings = "Einstellungen";
                    XYZMainForm.StrLengthShaftX = "Länge X Bar mm";
                    XYZMainForm.StrLengthShaftY = "Länge Y Bar mm";
                    XYZMainForm.StrMargeX = "linker Rand X mm";
                    XYZMainForm.StrMargeY = "top Rand Y mm";

                    break;
                case 3:                       // English
                    XYZMainForm.StrFile = "File";

                    XYZMainForm.StrOpenFile = "Open HPGL file";
                    XYZMainForm.StrOpenGCODEFile = "Open *.gcode file";
                    XYZMainForm.StrOpenDrillFile = "Open a drill (*.drl) file";
                    XYZMainForm.StrSaveFile = "Save HPGL file";
                    XYZMainForm.StrLoadModel = "Load model image";
                    XYZMainForm.StrClearModel = "Clear model image";
                    XYZMainForm.StrClearListbox = "Clear Listbox";
                    XYZMainForm.StrClearDrawing = "Clear drawing";
                    XYZMainForm.StrClearAll = "Clear all";

                    XYZMainForm.StrMirror = "Y mirror";
                    XYZMainForm.StrDrawing = "Drawing";
                    XYZMainForm.StrUndo = "Undo";
                    XYZMainForm.StrUndoSteps = "Undo steps:";
                    XYZMainForm.StrColor = "Color";
                    XYZMainForm.StrThickness = "Thickness";
                    
                    XYZMainForm.StrDraw = "Draw";

                    XYZMainForm.StrFoot = "Turn above checkbox 'Drawing' on.    Draw freehand while left mousebutton is pressed";

                    XYZMainForm.StrCommand = "Command";

                    XYZMainForm.StrMenu = "Menu";
                    XYZMainForm.StrDelay = "delay";
                    XYZMainForm.StrReset = "Reset";
                    XYZMainForm.StrZSteps = "Z steps";
                    XYZMainForm.StrUp = "Up";
                    XYZMainForm.StrDown = "Down";

                    XYZMainForm.StrCut = "Cutter";
                    XYZMainForm.StrDrill = "Drill";
                    XYZMainForm.StrScreen = "Screen";
                    XYZMainForm.StrView = "View";
                    XYZMainForm.StrPrint = "Print";
                    XYZMainForm.StrDelete = "Delete";
                    XYZMainForm.StrQuit = "Quit";
                    XYZMainForm.StrHelp = "Help";
                    XYZMainForm.StrSave = "Save";
                    XYZMainForm.StrSaveNew = "Save as new";
                    XYZMainForm.StrSetup = "Setup";

                    XYZMainForm.StrStepperResolution = "Stepper motor resolution";
                    XYZMainForm.StrColorButton = "Color button";
                    XYZMainForm.StrColorForm = "Color form";
                    XYZMainForm.StrColorFond = "Color fond";

                    XYZMainForm.StrManual = "Manual";
                    XYZMainForm.StrOnlineManual = "Online manual";
                    XYZMainForm.StrContactUs = "Contact us";
                    XYZMainForm.StrVersion = "Version 1.1  31/01/2018";
                    XYZMainForm.StrAutor = "Auteur";
                    XYZMainForm.StrStart = "Start";
                    XYZMainForm.StrIconColor = "Icon color";       // = also for button color
                    XYZMainForm.StrBackColor = "Backcolor";
                    XYZMainForm.StrComport = "Comport";
                    XYZMainForm.StrClear = "Clear";
                    XYZMainForm.StrPrevious = "Previous";
                    XYZMainForm.StrCancel = "Cancel";
                    XYZMainForm.StrDefault = "Default";
                    XYZMainForm.StrNoSerialPorts = "No serial port founds";
                    XYZMainForm.StrNotFound = "not found";
                    XYZMainForm.StrAttention = "Attention";
                    XYZMainForm.StrSelection = "Selection";
                    XYZMainForm.StrSaveAndQuit = "Save";
                    XYZMainForm.StrLaunch = "Launch";
                    XYZMainForm.StrImport = "Import from";

                    XYZMainForm.StrStartPlot = "Start plotter";
                    XYZMainForm.StrStartCut = "Start cutter";
                    XYZMainForm.StrStartDrill = "Start drill";
                    XYZMainForm.StrStartDispense = "Start dispenser";
                    XYZMainForm.StrStartMill = "Start milling";

                    XYZMainForm.StrReopen = "Reopen";
                    XYZMainForm.StrPause = "Pause";
                    XYZMainForm.StrDimension = "Dimensions";


                    XYZMainForm.StrBT = "Selection of the Bluetooth outgoing comport.\n" +
                                "Do not enter, type the comport, but click on the downwards\n" +
                                "oriented  ^  sign and select the correct outgoing comport from the dropdown list.\n" +
                                "\n" +
                              "If no comport is in the dropdownlist, verify that the apparatus is ON\n" +
                              "and paired.\n" +
                             "\n" +
                             "Correct outgoing BT comportnumber is not known yet:\n" +
                            "'Select show Bluetooth devices' -> 'More bluetooth options'\n" +
                            " to see the correct outgoing comportnumber\n" +
                            " select this COMx nr in the dropdown list.\n";

                    XYZMainForm.StrUSB = "Please, do not enter, type the comport here, but click on the downwards\n" +
                                 "oriented  ^  sign and select the correct comport from the dropdown list.\n" +
                                 "\n" +
                                 "If no comport is in the dropdownlist, the apparatus is not connected\n" +
                                 "or the driver is not yet installed.\n" +
                                 "\n" +
                                "If the correct comportnumber is not known yet,\n" +
                                "click twice on the downwards oriented  ^  sign :\n" +
                                "- once without the apparatus connected to USB\n" +
                                "- once with the apparatus connected to the USB port\n" +
                                "the comport that only appears when the apparatus is connected, is the correct choice.\n"+
                                "\n" +
                                "First time installation may take some time, even on a new PC.\n";

                    //   MainForm.StrLanguage = "Version 6.7 for Ecg F12 and L12. Release  17/10/2017";
                    //      MainForm.StrOpenSetup = "First use, open setup - icon with the key";

                    XYZMainForm.StrHPGLInputProperties = "HPGL input properties";
                    XYZMainForm.StrScreenCoordinates = "Screen coordinates, drawing made with this program";
                    XYZMainForm.StrIndependent = "The plot dimensions are not dependent on the paper size";
                    XYZMainForm.StrDependent = "The plot dimensions depend on the paper size";
                    XYZMainForm.StrDoNotScale = "Do not scale to output (PCB)";
                    XYZMainForm.StrDoScale = "Scale to output paper or vinyl";
                    XYZMainForm.StrPrecision = "Precision";

                    XYZMainForm.StrScrCoord = "Screen coordinates";
                    XYZMainForm.StrSetOther = "Set other input or output format";
                    XYZMainForm.StrScaled = "Scaled output";

                    XYZMainForm.StrSelectPad = "Select a pad:";
                    XYZMainForm.StrSetProperties = "Set properties:";
                    XYZMainForm.StrDotDistance = "Dot distance";
                    XYZMainForm.StrDotExtrusion = "Dot extrusion";
                    XYZMainForm.StrSet = "Set";
                    XYZMainForm.StrFileContent = "File content:";

                    XYZMainForm.StrSettings = "Settings";
                    XYZMainForm.StrLengthShaftX = "Lenght X shaft mm";
                    XYZMainForm.StrLengthShaftY = "Length Y shaft mm";
                    XYZMainForm.StrMargeX = "Left marge X mm";
                    XYZMainForm.StrMargeY = "Top marge Y mm";

                    break;
                case 4:
                    XYZMainForm.StrFile = "Fichier";
                    
                    XYZMainForm.StrOpenFile = "Ouvrir le fichier hpgl";
                    XYZMainForm.StrOpenGCODEFile = "Ouvrir le fichier *.gcode";
                    XYZMainForm.StrOpenDrillFile = "Ouvrir un fichier de forage (*.drl)"; // ou percer ?
                    XYZMainForm.StrSaveFile = "Sauvegarder fichier HPGL";
                    XYZMainForm.StrLoadModel = "Charger une image de modèle";
                    XYZMainForm.StrClearModel = "Effacer l'image du modèle";
                    XYZMainForm.StrClearListbox = "Effacer le liste";
                    XYZMainForm.StrClearDrawing = "Effacer le dessin";
                    XYZMainForm.StrClearAll = "Effacer tout";

                    XYZMainForm.StrMirror = "Y en miroir";
                    XYZMainForm.StrDrawing = "Dessiner";
                    XYZMainForm.StrUndo = "Reculer";
                    XYZMainForm.StrUndoSteps = "nombre de pas:";
                    XYZMainForm.StrColor = "Couleur";
                    XYZMainForm.StrThickness = "Épaisseur de ligne";
                    
                    XYZMainForm.StrFoot = "Crocher 'Dessiner' au dessus.     Dessinez en appuyant sur le bouton gauche de la souris.";

                      //  "Turn above checkbox 'Drawing' on.   Draw freehand while left mousebutton is pressed";


                    XYZMainForm.StrDraw = "Dessiner";
                    XYZMainForm.StrCommand = "Commander";


                    XYZMainForm.StrMenu = "Menu";
                    XYZMainForm.StrDelay = "ralentir";
                    XYZMainForm.StrReset = "Réinitialiser";
                    XYZMainForm.StrZSteps = "Z pas";   // marches ?
                    XYZMainForm.StrUp = "Vers le haut";    //  Up";
                    XYZMainForm.StrDown = "Vers le bas";   //  Down";


                    XYZMainForm.StrCut = "Couper";
                    XYZMainForm.StrDrill = "Forage";
                    XYZMainForm.StrScreen = "Écran";
                    XYZMainForm.StrView = "Vue";
                    XYZMainForm.StrPrint = "Imprimer";
                    XYZMainForm.StrDelete = "Supprimer";
                    XYZMainForm.StrQuit = "Quitter";
                    XYZMainForm.StrHelp = "Aide";
                    XYZMainForm.StrSave = "Sauvegarder";
                    XYZMainForm.StrSaveNew = "Sauvegarder comme";
                    XYZMainForm.StrSetup = "Configuration";
                    
                    XYZMainForm.StrStepperResolution = "Résolution des moteurs XY pas à pas";
                    XYZMainForm.StrColorButton = "Couleur des boutons";
                    XYZMainForm.StrColorForm = "Couleur du formulaire";  // ? Form color
                    XYZMainForm.StrColorFond = "Couleur de fond";

                    XYZMainForm.StrManual = "Manuel";
                    XYZMainForm.StrOnlineManual = "Online manuel";
                    XYZMainForm.StrContactUs = "Contactez-nous";
                    XYZMainForm.StrVersion = "Version 1.1  31/01/2018";
                    XYZMainForm.StrAutor = "Auteur";
                    XYZMainForm.StrStart = "Démarrer";
                    XYZMainForm.StrIconColor = "Couleur icône";       // = also for button color
                    XYZMainForm.StrBackColor = "Couleur fond";
                    XYZMainForm.StrComport = "Comport";
                    XYZMainForm.StrClear = "Éraser";
                    XYZMainForm.StrPrevious = "Précédent";
                    XYZMainForm.StrCancel = "Anuler";
                    XYZMainForm.StrDefault = "Défaut";
                    XYZMainForm.StrNoSerialPorts = "Port série pas trouvé";
                    XYZMainForm.StrNotFound = "pas trouvé";
                    XYZMainForm.StrAttention = "Attention";
                    XYZMainForm.StrSelection = "Sélection";
                    XYZMainForm.StrSaveAndQuit = "Sauvegarder et quitter";
                    XYZMainForm.StrLaunch = "Démarrer";
                    XYZMainForm.StrImport = "Importer";
                   
                   
                    XYZMainForm.StrStartPlot = "Démarrer";
                    XYZMainForm.StrStartCut = "Démarrer";
                    XYZMainForm.StrStartDrill = "Démarrer";   // "Start drill";
                    XYZMainForm.StrStartDispense = "Démarrer dispenser";
                    XYZMainForm.StrStartMill = "Démarrer fraisage";


                    XYZMainForm.StrReopen = "Rouvrir";
                    XYZMainForm.StrPause = "Pause";
                    XYZMainForm.StrDimension = "Dimensions";
                    
                    XYZMainForm.StrUSB =
                        "Veuillez saisir le numéro du port série en cliquant\n" +
                        "sur la petite flèche  ^ vers inférieur.\n" +
                        "\n" +
                        "Si vous ne savez pas le numéto correct, cliquez deux fois\n" +
                        "sur  ^ vers inférieur, une fois sans l'appareil connecté vers\n" +
                        "le PC et une fois avec l'appareil connecté.\n" +
                        "Le port qui apparêit avec l'appareil connecté est le choix correct\n" +
                        "\n" +
                        "La première installation peut durer plusieurs minutes.";
                                       

                    XYZMainForm.StrHPGLInputProperties = "Propriétés HPGL";
                    XYZMainForm.StrScreenCoordinates = "Coordonnées de l'écran, dessin réalisé avec ce logiciel";
                    XYZMainForm.StrIndependent = "Les dimensions du plot ne dépendent pas de la taille du papier (PCB)";
                    XYZMainForm.StrDependent = "Les dimensions du plot dépendent de la taille du papier, A1,A2..";
                    XYZMainForm.StrDoNotScale = "Taille vraie. (PCB)";
                    XYZMainForm.StrDoScale = "Échelle selon les dimensions du papier";
                    XYZMainForm.StrPrecision = "Précision";

                    XYZMainForm.StrScrCoord = "Coordonnées de l'écran";
                    XYZMainForm.StrSetOther = "Autre format d'entrée ou de sortie";
                    XYZMainForm.StrScaled = "Sortie à l'échelle";

                    XYZMainForm.StrSelectPad = "Sélectionnez un pad:";
                    XYZMainForm.StrSetProperties = "Définir les propriétés:";
                    XYZMainForm.StrDotDistance = "Dot distance";
                    XYZMainForm.StrDotExtrusion = "Dot extrusion";
                    XYZMainForm.StrSet = "Appliquer";
                    XYZMainForm.StrFileContent = "Contenu du fichier:";

                    XYZMainForm.StrSettings = "Configuration";
                    XYZMainForm.StrLengthShaftX = "Longueur X bar mm";
                    XYZMainForm.StrLengthShaftY = "Longueur Y bar mm";
                    XYZMainForm.StrMargeX = "Marge gauche X mm";
                    XYZMainForm.StrMargeY = "Marge supérieure Y mm";
                    
                    break;
                case 5:
                    
                    XYZMainForm.StrFile = "Archivo";
                    XYZMainForm.StrOpenFile = "Abrir el archivo HPGL";
                    XYZMainForm.StrOpenGCODEFile = "Abrir el archivo *.gcode";
                    XYZMainForm.StrOpenDrillFile = "Abrir el archivo de perforación (*.drl)"; //archivo de perforación
                    XYZMainForm.StrSaveFile = "Guardar archivo HPGL";
                    XYZMainForm.StrLoadModel = "Cargar una imagen modelo";
                    XYZMainForm.StrClearModel = "Borrar la imagen del modelo";
                    XYZMainForm.StrClearListbox = "Borrar el listbox";
                    XYZMainForm.StrClearDrawing = "Eliminar dibujo";
                    XYZMainForm.StrClearAll = "Borrar todo";

                    XYZMainForm.StrMirror = "Y espejo";
                    XYZMainForm.StrDrawing = "Dibujar";
                    XYZMainForm.StrUndo = "Deshacer";
                    XYZMainForm.StrUndoSteps = "Numero de pasos";
                    XYZMainForm.StrColor = "Color";
                    XYZMainForm.StrThickness = "Espesor de línea";

                    XYZMainForm.StrFoot = "Primero marque la casilla de verificación 'Dibujar' en la parte superior.  Puede dibujar libremente en la pantalla presionando el botón izquierdo del ratón";

                    XYZMainForm.StrDraw = "Dibujar";
                    
                //    MainForm.StrDraw = "Diseño";
                    XYZMainForm.StrCommand = "Comandor";
                    
                    XYZMainForm.StrMenu = "Menú";
                    XYZMainForm.StrDelay = "retraso";
                    XYZMainForm.StrReset = "Reiniciar";
                    XYZMainForm.StrZSteps = "Z pasos"; // subir y bajar
                    XYZMainForm.StrUp = "Arriba";
                    XYZMainForm.StrDown = "Abajo";

                    XYZMainForm.StrCut = "Cortador";
                    XYZMainForm.StrDrill = "Perforaçion";
                    XYZMainForm.StrScreen = "Pantalla";
                    XYZMainForm.StrView = "Ver";
                    XYZMainForm.StrPrint = "Imprimar";
                    XYZMainForm.StrDelete = "Delete";
                    XYZMainForm.StrQuit = "Salir";
                    XYZMainForm.StrHelp = "Ayuda";
                    XYZMainForm.StrSave = "Archivar";
                    XYZMainForm.StrSaveNew = "Archivar como nuevo";
                    XYZMainForm.StrSetup = "Configuración";

                    XYZMainForm.StrStepperResolution = "Resolución de motor paso a paso";
                    XYZMainForm.StrColorButton = "Color del botón";
                    XYZMainForm.StrColorForm = "Color de la forma";
                    XYZMainForm.StrColorFond = "Color de aficionado";

                    XYZMainForm.StrManual = "Manual";  // ?
                    XYZMainForm.StrOnlineManual = "Online manual";
                    XYZMainForm.StrContactUs = "Contact us";
                    XYZMainForm.StrVersion = "Version 1.1  31/01/2018";
                    XYZMainForm.StrAutor = "Auteur";
                    XYZMainForm.StrStart = "Empezar";
                    XYZMainForm.StrIconColor = "Color de icono";       // = also for button color
                    XYZMainForm.StrBackColor = "Color de fondo";
                    XYZMainForm.StrComport = "Comport";
                    XYZMainForm.StrClear = "Clear";
                    XYZMainForm.StrPrevious = "Previous";
                    XYZMainForm.StrCancel = "Cancel";
                    XYZMainForm.StrDefault = "Default";
                    XYZMainForm.StrNoSerialPorts = "No serial port founds";
                    XYZMainForm.StrNotFound = "not found";
                    XYZMainForm.StrAttention = "Attention";
                    XYZMainForm.StrSelection = "Selection";
                    XYZMainForm.StrSaveAndQuit = "Salir"; // Archivar e salir
                    XYZMainForm.StrLaunch = "Iniciar";
                    XYZMainForm.StrImport = "Importar desde";
                                      
                    XYZMainForm.StrLaunchFileViewer = "Launch file viewer";
                    
                    XYZMainForm.StrStartPlot = "Comenzar";                   
                    XYZMainForm.StrStartCut =  "Comenzar";    // "Start cutter";
                    XYZMainForm.StrStartDrill = "Comenzar";    //  "Start drill";                                        
                    XYZMainForm.StrStartDispense = "Comenzar dispenser";
                    XYZMainForm.StrStartMill = "Comenzar molienda";
                    


                    XYZMainForm.StrReopen = "Reabrir";
                    XYZMainForm.StrPause = "Pausa";
                    XYZMainForm.StrDimension = "Dimensión";
                  
                    XYZMainForm.StrUSB = "Please, do not enter, type the comport here, but click on the downwards\n" +
                                 "oriented  ^  sign and select the correct comport from the dropdown list.\n" +
                                 "\n" +
                                 "If no comport is in the dropdownlist, the apparatus is not connected\n" +
                                 "or the driver is not yet installed.\n" +
                                 "\n" +
                                "If the correct comportnumber is not known yet,\n" +
                                "click twice on the downwards oriented  ^  sign :\n" +
                                "- once without the apparatus connected to USB\n" +
                                "- once with the apparatus connected to the USB port\n" +
                                "the comport that only appears when the apparatus is connected, is the correct choice\n" +
                                "\n" +
                                "First time installation may take some time, even on a new PC.\n"+
                                 "Selecting wrong comport = no operation of hardware !";

                 
                    XYZMainForm.StrHPGLInputProperties = "Propiedades de entrada HPGL";
                    XYZMainForm.StrScreenCoordinates = "Coordenadas de pantalla, dibujo realizado con este programa";
                    XYZMainForm.StrIndependent = "Las dimensiones plot no dependen del tamaño del papel";
                    XYZMainForm.StrDependent = "Las dimensiones plot dependen del tamaño del papel";
                    XYZMainForm.StrDoNotScale = "No escalar.(PCB)";
                    XYZMainForm.StrDoScale = "Escalar para imprimir en papel o vinilo";
                    XYZMainForm.StrPrecision = "Precisión";

                    XYZMainForm.StrScrCoord = "Coordinades de pantalla";
                    XYZMainForm.StrSetOther = "Otro formato de entrada o salida";
                    XYZMainForm.StrScaled = "Escalada";


                    XYZMainForm.StrSelectPad = "Seleccionar un pad:";
                    XYZMainForm.StrSetProperties = "Propiedades :";
                    XYZMainForm.StrDotDistance = "Dot distancia";
                    XYZMainForm.StrDotExtrusion = "Extrusión de paste";
                    XYZMainForm.StrSet = "Aplicar";
                    XYZMainForm.StrFileContent = "Contenido del archivo: ";

                    XYZMainForm.StrSettings = "Configuraciones";
                    XYZMainForm.StrLengthShaftX = "Longitud X barra mm";
                    XYZMainForm.StrLengthShaftY = "Longitud Y barra mm";
                    XYZMainForm.StrMargeX = "Margen izquierdo X mm";
                    XYZMainForm.StrMargeY = "Margen superior Y mm";


                    break;
                default: break;
            }


        }

    }
}
