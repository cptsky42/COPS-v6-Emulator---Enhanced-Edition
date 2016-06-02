--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:40 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask10006(client, idx)
    name = "Warehouseman"
    face = 1

    if (idx == 0) then

        text(client, "Welcome! I run a warehouse in every city. You can store your money and items in my warehouses, and retrieve them for free.")
        link(client, "How to use the warehouse.", 1)
        link(client, "Consult others.", 255)
        pic(client, 10)
        create(client)

    elseif (idx == 1) then

        text(client, "To deposit money, just click on me, enter the amount, and then click on Deposit. Withdrawing is in the same way.")
        text(client, "To store an item, just drag it to my slots and then release it. To take it out, just click on it. There is one")
        text(client, "warehouse available in each city and the market. You had better store your valuable items and do not carry too much money.")
        link(client, "I see. Thanks.", 255)
        pic(client, 10)
        create(client)

    end

end
