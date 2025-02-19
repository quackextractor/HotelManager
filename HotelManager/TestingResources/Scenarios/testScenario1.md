# **SPSE Jecna Test Case**  
**Test Case ID:** APP_01  
**Test Designed by:** Miro Slezák  
**Test Name:** Application Launch and Database Structure Import  
**Brief description:** Verify successful application launch, configuration validation, and database structure import via XML.

---

### **Pre-conditions**
1. `HotelManager` is installed on the system.
2. `complex_tableset.xml` file is available in a known directory.
3. SQL Server Express LocalDB is installed and running.
4. Connection string in `App.config` is correctly configured.

---

### **Dependencies and Requirements**
**Software:**
- Windows OS (10/11)
- .NET Framework 4.7.2+
- SQL Server Express LocalDB  
  **Hardware:**
- 2 GHz CPU, 4 GB RAM, 500 MB disk space  
  **Other:**
- `complex_tableset.xml` (sample dataset) from /TestingResources/DataSets/

---

| **Step** | **Test Steps**                                                           | **Test Data**                          | **Expected Result**                                                              | **Notes**                                                                 |
|----------|--------------------------------------------------------------------------|----------------------------------------|----------------------------------------------------------------------------------|---------------------------------------------------------------------------|
| 1        | Launch the application.                                                  | `HotelManager.exe`                     | Main window appears with a loading panel, followed by the main menu.             | Ensure no firewall blocks the application.                               |
| 2        | Click **"Import Tabulek"** button on the main menu.                      | N/A                                    | `DataLoaderForm` opens with file browse/drag-and-drop options.                   | Verify UI responsiveness.                                                |
| 3        | Import `complex_tableset.xml` via drag-and-drop or **Procházet** button. | `complex_tableset.xml` (valid structure) | File path is displayed in the text field.                                        | If XML is invalid, show error message.                                   |
| 4        | Click **"Načíst"** to import data.                                       | N/A                                    | "Data imported successfully!" message. Database tables are created in `HotelDB`. | Check SQL Server Object Explorer for `Rooms`, `Persons`, `Orders` tables.|
| 5        | Click **"Vyhledat"** to search tables.                                   | N/A                                    | `SearchForm` Opens with search option dropdown                                   | Ensure no errors occur during navigation.                                |
| 6        | Click **"Vyhledat"** again to look through all order tables.             | N/A                                    | Imported orders and their contents get listed                                    | Ensure no errors occur during navigation.                                |
| 7        | Double click on a cell to further verify data                            | N/A                                    | Opens `EditOrderForum` and displays data                                         | Ensure no errors occur during navigation.                                |

---

**Validation Criteria:**
- Database tables match the schema defined in `complex_tableset.xml`.
- Application UI reflects imported data (e.g., rooms listed in dropdowns).
- No errors in logs during import.