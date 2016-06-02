--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:50 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask30159(client, idx)
    name = "Carpenter"
    face = 1

    if (idx == 0) then

        text(client, "I am here to issue all kinds of timber Vouchers. Are you here for the Vouchers?")
        link(client, "Yeah.", 1)
        link(client, "Just passing by.", 255)
        pic(client, 92)
        create(client)

    elseif (idx == 1) then

        text(client, "I can issue a Rosewood Voucher for 10 pieces of timber, and a Timber Voucher for 10 Rosewood Vouchers. What do you want?")
        link(client, "Rosewood Voucher.", 2)
        link(client, "Timber Voucher.", 3)
        link(client, "I changed my mind.", 255)
        pic(client, 92)
        create(client)

    elseif (idx == 2) then

        if hasItem(client, 721171, 10) then

            if spendItem(client, 721171, 10) then

                awardItem(client, "721172", 1)

            else

                text(client, "Sorry, you do not have 10 pieces of timber. I heard that Craftsman is supplying timber.")
                link(client, "I see.", 255)
                pic(client, 92)
                create(client)

            end

        else

            text(client, "Sorry, you do not have 10 pieces of timber. I heard that Craftsman is supplying timber.")
            link(client, "I see.", 255)
            pic(client, 92)
            create(client)

        end

    elseif (idx == 3) then

        if hasItem(client, 721172, 10) then

            if spendItem(client, 721172, 10) then

                awardItem(client, "721173", 1)

            else

                text(client, "Sorry, you do not have 10 Rosewood Vouchers.")
                link(client, "I see.", 255)
                pic(client, 92)
                create(client)

            end

        else

            text(client, "Sorry, you do not have 10 Rosewood Vouchers.")
            link(client, "I see.", 255)
            pic(client, 92)
            create(client)

        end

    end

end
