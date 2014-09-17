using iBizProduct.DataContracts;

namespace iBizProduct.Ultilities
{
    /// <summary>
    /// Provides static utilities for working with Product Orders
    /// </summary>
    public class ProductOrderUtility
    {
        /// <summary>
        /// Returns what the Product Order Status should be updated to based on a Product Order's current Order Status and what the Backend
        /// Event to perform is.
        /// </summary>
        /// <param name="CurrentOrderStatus">Current ProductOrder Status</param>
        /// <param name="ActionToPerform">What Event Action to perform</param>
        /// <returns>New ProductOrder Status</returns>
        public static ProductOrderStatus GetNewOrderStatus( ProductOrderStatus CurrentOrderStatus, EventActions ActionToPerform )
        {
            switch( CurrentOrderStatus )
            {
                case ProductOrderStatus.ACTIVE:
                    switch( ActionToPerform )
                    {
                        case EventActions.Delete:
                            return ProductOrderStatus.IN_PROGRESS_DELETE;
                        case EventActions.Suspend:
                            return ProductOrderStatus.IN_PROGRESS_SUS;
                        default:
                            return CurrentOrderStatus;
                    }
                case ProductOrderStatus.COMPLETE:
                    switch( ActionToPerform )
                    {
                        case EventActions.Activate:
                            return ProductOrderStatus.ACTIVE;
                        case EventActions.Delete:
                            return ProductOrderStatus.IN_PROGRESS_DELETE;
                        default:
                            return CurrentOrderStatus;
                    }
                case ProductOrderStatus.IN_PROGRESS_ADD:
                    switch( ActionToPerform )
                    {
                        case EventActions.Activate:
                            return ProductOrderStatus.ACTIVE;
                        case EventActions.Delete:
                            return ProductOrderStatus.IN_PROGRESS_DELETE;
                        case EventActions.Suspend:
                            return ProductOrderStatus.IN_PROGRESS_SUS;
                        default:
                            return CurrentOrderStatus;
                    }
                case ProductOrderStatus.IN_PROGRESS_DELETE:
                    return CurrentOrderStatus;
                //case ProductOrderStatus.IN_PROGRESS_EDIT:
                //    break;
                //case ProductOrderStatus.IN_PROGRESS_INV:
                //    break;
                case ProductOrderStatus.IN_PROGRESS_SUS:
                    switch( ActionToPerform )
                    {
                        case EventActions.Activate:
                            return ProductOrderStatus.ACTIVE;
                        case EventActions.Delete:
                            return ProductOrderStatus.IN_PROGRESS_DELETE;
                        default:
                            return CurrentOrderStatus;
                    }
                    break;
                case ProductOrderStatus.IN_PROGRESS_UNSUS:
                    switch( ActionToPerform )
                    {
                        case EventActions.Activate:
                            return ProductOrderStatus.ACTIVE;
                        case EventActions.Delete:
                            return ProductOrderStatus.IN_PROGRESS_DELETE;
                        case EventActions.Suspend:
                            return ProductOrderStatus.IN_PROGRESS_SUS;
                        default:
                            return CurrentOrderStatus;
                    }
                    break;
                case ProductOrderStatus.INCOMPLETE:
                    switch( ActionToPerform )
                    {
                        case EventActions.Activate:
                            return ProductOrderStatus.ACTIVE;
                        case EventActions.Delete:
                            return ProductOrderStatus.IN_PROGRESS_DELETE;
                        default:
                            return CurrentOrderStatus;
                    }
                case ProductOrderStatus.SUSPENDED:
                    switch( ActionToPerform )
                    {
                        case EventActions.Activate:
                            return ProductOrderStatus.ACTIVE;
                        case EventActions.Delete:
                            return ProductOrderStatus.IN_PROGRESS_DELETE;
                        default:
                            return CurrentOrderStatus;
                    }
                default:
                    throw new iBizException( "An unknown error occurred. Please report this bug to the code monkey in charge." );
            }
        }
    }
}
