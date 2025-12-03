public class PaymobCallbackDto
{
    public string order { get; set; }
    public bool success { get; set; }
    public int amount_cents { get; set; }
    public string transaction_id { get; set; }
    public string hmac { get; set; }
    public string? error_occured { get; set; }

    public string created_at { get; set; }
    public string currency { get; set; }
    public string has_parent_transaction { get; set; }
    public string id { get; set; }
    public string integration_id { get; set; }
    public string is_3d_secure { get; set; }
    public string is_auth { get; set; }
    public string is_capture { get; set; }
    public string is_refunded { get; set; }
    public string is_standalone_payment { get; set; }
    public string is_voided { get; set; }
    public string owner { get; set; }
    public string pending { get; set; }
    public string source_data_pan { get; set; }
    public string source_data_sub_type { get; set; }
    public string source_data_type { get; set; }
}
