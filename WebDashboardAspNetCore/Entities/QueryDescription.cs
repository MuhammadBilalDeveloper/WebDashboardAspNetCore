namespace WebDashboardAspNetCore.Entities
{
    public class QueryDescription
    {
        public enum enAttributeLevel { Flight = 1, Stop = 2, TransferFlight = 3 }

        // QRY_ID
        public int QueryId { get; set; }

        // QRY_SEL_ID
        public int QuerySelectedId { get; set; }

        // BASIC_ATTR_ID
        public int BasicAttributeId { get; set; }

        // BASIC_ATTR_TYPE
        public string BasicAttributeType { get; set; }

        // BASIC_ATTR_NAME
        public string BasicAttributeName { get; set; }

        // LEVEL_TYPE
        public string LevelType { get; set; }

        // LEVEL_ID
        public int LevelId { get; set; }

        // LEVEL_NAME
        public string LevelName { get; set; }

        // ATTR_INFO_TYPE: ATTR_TYPE_CODE
        public string AtrributeTypeCode
        {
            get; set;
        }

        // RS_NAME
        public string RsName
        {
            get; set;
        }

        // RS_DISPLAY_NAME
        public string RsDisplayName
        {
            get; set;
        }

        // DISPLAY_FORMAT
        public string DisplayFormat
        {
            get; set;
        }

        // ORDNO
        public int OrdNo { get; set; }

        // ATTR_LVL 1 - flight, 2 - stop, 3 - transfer flight
        public int AttributeLevel { get; set; }

        // Replacedment for ATTR_INFO_TYPE now json will be parsed for these Columns i.attr_type_code AtrributeTypeCode, i.rs_name RsName, i.rs_display_name RsDisplayName, i.display_format DisplayFormat 
        public string attr_info_type { get; set; }
    }
}
