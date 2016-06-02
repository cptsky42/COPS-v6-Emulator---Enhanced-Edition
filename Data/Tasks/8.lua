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

function processTask8(client, idx)
    name = "Warehouseman"
    face = 1

    if (idx == 0) then

        if checkLockPin(client) then

            sendSysMsg(client, "To secure your possessions, you are recommended to set a Warehouse Lock with WhsGuardian [409 392] in Twin City.", 2005)
            openDialog(client, 4)

        else

            if getRegister(client, 0) == 2 then

                openDialog(client, 4)

            else

                if getRegister(client, 1) >= 3 then

                    text(client, "You have input the wrong passwords for three successive times. You must re-login if you wanna use the warehouse.")
                    link(client, "That is ok.", 255)
                    pic(client, 67)
                    create(client)

                else

                    text(client, "You have created a password for your warehouse. Please input the password to open it.")
                    edit(client, "", 1, 9)
                    link(client, "Let me see.", 255)
                    pic(client, 67)
                    create(client)

                end

            end

        end

    elseif (idx == 1) then

        if checkLockPin(client) then

            setRegister(client, 0, 2)
            openDialog(client, 4)

        else

            if getRegister(client, 1) == 0 then

                setRegister(client, 1, 1)
                text(client, "Wrong password. Enter again.")
                link(client, "Ok.", 2)
                link(client, "Let me see.", 3)
                pic(client, 67)
                create(client)

            else

                if getRegister(client, 1) == 1 then

                    setRegister(client, 1, 2)
                    text(client, "Wrong password. Enter again.")
                    link(client, "Ok.", 2)
                    link(client, "Let me see.", 3)
                    pic(client, 67)
                    create(client)

                else

                    if getRegister(client, 1) == 2 then

                        setRegister(client, 1, 3)
                        text(client, "You have input the wrong passwords for three successive times. You must re-login if you wanna use the warehouse.")
                        link(client, "That is ok.", 255)
                        pic(client, 67)
                        create(client)

                    end

                end

            end

        end

    elseif (idx == 2) then

        if getRegister(client, 1) >= 3 then

            text(client, "You have input the wrong passwords for three successive times. You must re-login if you wanna use the warehouse.")
            link(client, "That is ok.", 255)
            pic(client, 67)
            create(client)

        else

            text(client, "You have created a password for your warehouse. Please input the password to open it.")
            edit(client, "", 1, 9)
            link(client, "Let me see.", 255)
            pic(client, 67)
            create(client)

        end

    end

end
