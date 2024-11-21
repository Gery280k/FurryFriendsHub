function renderPayPalButtonWithTotal(total) {
    paypal.Buttons({
        createOrder: function (data, actions) {
            return actions.order.create({
                purchase_units: [{
                    amount: {
                        value: total.toFixed(2)
                    }
                }]
            });
        },
        onApprove: function (data, actions) {
            return actions.order.capture().then(function (details) {
                alert('Pago completado por ' + details.payer.name.given_name);
                console.log(details);
            });
        }
    }).render('#paypal-button-container');
}
