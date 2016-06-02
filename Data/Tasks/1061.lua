--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 4/20/2015 7:11:18 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask1061(client, idx)
    name = "WhsGuardian"
    face = 1

    if (idx == 0) then

        if checkLockPin(client) then

            text(client, "To protect your property, I recommend you to create a password for your warehouse. So every time you login, you")
            text(client, "must check password with Warehouseman in order to use it. If you wanna change or cancel the password, just come to me.")
            link(client, "I want to set a password.", 1)
            link(client, "Let me see.", 255)
            pic(client, 4)
            create(client)

        else

            text(client, "You have created a password for your warehouse successfully. If you wanna change or cancel the password, just come to me.")
            link(client, "I wanna change it.", 2)
            link(client, "OK. Thanks.", 255)
            pic(client, 4)
            create(client)

        end

    elseif (idx == 1) then

        text(client, "A password must consist of numbers up to 9 bytes, and the first digit could not be 0. If you enter a character other")
        text(client, "than numbers, only the valid numbers before it will work as the password. Please check carefully before you enter the password.")
        edit(client, "", 3, 9)
        link(client, "Let me think.", 255)
        pic(client, 4)
        create(client)

    elseif (idx == 2) then

        if getRegister(client, 1) >= 3 then

            text(client, "You have input the wrong passwords for three successive times. You must re-login if you wanna use the warehouse.")
            link(client, "OK.", 255)
            pic(client, 4)
            create(client)

        else

            text(client, "Please enter your old password in order to change it.")
            edit(client, "", 4, 9)
            link(client, "Let me think.", 255)
            pic(client, 4)
            create(client)

        end

    elseif (idx == 3) then

        setLockPin(client)
        text(client, "Please verify your password. If you do not verify it, then I will take in your last password.")
        edit(client, "", 5, 9)
        pic(client, 4)
        create(client)

    elseif (idx == 4) then

        if checkLockPin(client) then

            text(client, "Do you want to change the password or cancel it?")
            link(client, "Change it.", 1)
            link(client, "Cancel it.", 6)
            pic(client, 4)
            create(client)

        else

            if getRegister(client, 1) == 0 then

                setRegister(client, 1, 1)
                text(client, "Wrong password. Please enter again.")
                link(client, "OK.", 2)
                link(client, "Let me think.", 255)
                pic(client, 4)
                create(client)

            else

                if getRegister(client, 1) == 1 then

                    setRegister(client, 1, 2)
                    text(client, "Wrong password. Please enter again.")
                    link(client, "OK.", 2)
                    link(client, "Let me think.", 255)
                    pic(client, 4)
                    create(client)

                else

                    if getRegister(client, 1) == 2 then

                        setRegister(client, 1, 3)
                        text(client, "You have input the wrong passwords for three successive times. You must re-login if you wanna use the warehouse.")
                        link(client, "OK.", 255)
                        pic(client, 4)
                        create(client)

                    end

                end

            end

        end

    elseif (idx == 5) then

        if checkLockPin(client) then

            text(client, "You have created a password for your warehouse successfully. Please keep it safe. If you forget it, you must ask GMs for help.")
            link(client, "Thank you.", 255)
            pic(client, 4)
            create(client)

        else

            text(client, "Your passwords do not match. Please enter again. Otherwise I will take in your first password.")
            link(client, "Alright.", 7)
            pic(client, 4)
            create(client)

        end

    elseif (idx == 6) then

        setLockPin(client)
        text(client, "You have canceled the password to your warehouse successfully. If you want, you can still come to me to set password.")
        link(client, "OK. Thanks.", 255)
        pic(client, 4)
        create(client)

    elseif (idx == 7) then

        setLockPin(client)
        text(client, "A password must consist of numbers up to 9 bytes, and the first digit could not be 0. If you enter a character other")
        text(client, "than numbers, only the valid numbers before it will work as the password. Please check carefully before you enter the password.")
        edit(client, "", 3, 9)
        link(client, "Let me think.", 255)
        pic(client, 4)
        create(client)

    end

end
