// Copyright (c) iBizVision - 2014
// Author: Dan Siegel


namespace iBizProduct.DataContracts
{
    /// <summary>
    /// Product Order Status
    /// </summary>
    public enum ProductOrderStatus
    {
        /// <summary>
        /// The order has been initially added to the system, but is not (necessarily) ready for activation.
        /// ie. there may be missing attributes.
        /// Rental property analogy: Renter wants to rent but hasn't selected a floor plan.
        /// Next state: COMPLETE
        /// </summary>
        INCOMPLETE,

        /// <summary>
        /// The order is in the system and can be activated at any time.
        /// Rental property analogy: Renter has selected a floor plan is ready to complete paperwork and pay down payment.
        /// Next state: IN_PROGRESS_INV -OR- IN_PROGRESS_ADD (if no inventory applicable)
        /// </summary>
        COMPLETE,

        /// <summary>
        /// This order has been activated, but is waiting for the inventory necessary to complete activation.
        /// Rental property analogy: Renter has completed paperwork and submitted the down payment and is now waiting for an available unit.
        /// Next state: IN_PROGRESS_ADD
        /// </summary>
        IN_PROGRESS_INV,

        /// <summary>
        /// This order has been activated but is waiting for provisioning to complete.
        /// Rental property analogy: Renter has completed the rental process, a unit is available, and is now waiting for the keys.
        /// Next state: ACTIVE
        /// </summary>
        IN_PROGRESS_ADD,

        /// <summary>
        /// This order is fully provisioned and being billed for (as necessary).
        /// Rental property analogy: Renter has completed the rental process and is paying their monthly lease.
        /// Next state: IN_PRORESS_EDIT -OR- IN_PROGRESS_SUS -OR- IN_PROGRESS_DELETE
        /// </summary>
        ACTIVE,

        /// <summary>
        /// This order has been activated but is waiting for provisioning to complete after an update.
        /// Rental property analogy: Renter has submitted a maintenance request and is waiting for manager to complete the work.
        /// Next state: ACTIVE
        /// </summary>
        IN_PROGRESS_EDIT,

        /// <summary>
        /// This order has been suspended but is waiting for provisioning to complete.
        /// Rental property analogy: Renter is late with lease payment and property manager needs to change the lock.
        /// Next state: SUSPENDED
        /// </summary>
        IN_PROGRESS_SUS,

        /// <summary>
        /// This order has been disabled such that service is not being rendered.
        /// Rental property analogy: Renter hasn't paid the rent and is now locked out of the building.
        /// Next state: IN_PROGRESS_UNSUS -OR- IN_PROGRESS_DELETE (if user deletes order while suspended)
        /// </summary>
        SUSPENDED,

        /// <summary>
        /// This order has been (re)activated (after a suspension) but is waiting for provisioning to complete.
        /// Rental property analogy: Renter has now paid the rent (late) and the manager needs to change the locks back so the renter has access to the unit.
        /// Next state: ACTIVE
        /// </summary>
        IN_PROGRESS_UNSUS,

        /// <summary>
        /// This order has been deleted but is waiting for provisioning to complete.
        /// Rental property analogy: Renter has notified manager of move-out, but out-processing need to be completed.
        /// Next state: N/A (The order will no longer exist)
        /// </summary>
        IN_PROGRESS_DELETE
    }
}
