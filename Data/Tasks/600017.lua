--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:53 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask600017(client, idx)
    name = "Ghost"
    face = 1

    if (idx == 0) then

        text(client, "I have been trapped here for many years. When can I see a person break through the tactics?")
        link(client, "How can I conquer?", 1)
        link(client, "What nonsense!", 255)
        pic(client, 9)
        create(client)

    elseif (idx == 1) then

        if hasItem(client, 721015, 1) then

            text(client, "If you do not have the tokens for the tactics other than Death and life, you cannot leave here until you die.")
            link(client, "I have all tokens.", 2)
            link(client, "What a bad luck!", 255)
            pic(client, 9)
            create(client)

        else

            text(client, "You are so weak. I am afraid you will die from the tactics.")
            link(client, "I must prove my ability.", 255)
            pic(client, 9)
            create(client)

        end

    elseif (idx == 2) then

        if hasItem(client, 721010, 1) then

            if hasItem(client, 721011, 1) then

                if hasItem(client, 721012, 1) then

                    if hasItem(client, 721013, 1) then

                        if hasItem(client, 721014, 1) then

                            if hasItem(client, 721015, 1) then

                                text(client, "Great! Would you like me to make a Soul Jade from your tokens so that you can enter Life Tactic to claim your prize?")
                                link(client, "Yes, Please.", 3)
                                link(client, "No, thanks.", 255)
                                pic(client, 9)
                                create(client)

                            else

                                text(client, "Sorry, you do not have the said tokens. I am afraid you cannot leave here until you die.")
                                link(client, "God bless me.", 255)
                                pic(client, 9)
                                create(client)

                            end

                        else

                            text(client, "Sorry, you do not have the said tokens. I am afraid you cannot leave here until you die.")
                            link(client, "God bless me.", 255)
                            pic(client, 9)
                            create(client)

                        end

                    else

                        text(client, "Sorry, you do not have the said tokens. I am afraid you cannot leave here until you die.")
                        link(client, "God bless me.", 255)
                        pic(client, 9)
                        create(client)

                    end

                else

                    text(client, "Sorry, you do not have the said tokens. I am afraid you cannot leave here until you die.")
                    link(client, "God bless me.", 255)
                    pic(client, 9)
                    create(client)

                end

            else

                text(client, "Sorry, you do not have the said tokens. I am afraid you cannot leave here until you die.")
                link(client, "God bless me.", 255)
                pic(client, 9)
                create(client)

            end

        else

            text(client, "Sorry, you do not have the said tokens. I am afraid you cannot leave here until you die.")
            link(client, "God bless me.", 255)
            pic(client, 9)
            create(client)

        end

    elseif (idx == 3) then

        spendItem(client, 721010, 1)
        spendItem(client, 721011, 1)
        spendItem(client, 721012, 1)
        spendItem(client, 721013, 1)
        spendItem(client, 721014, 1)
        spendItem(client, 721015, 1)
        move(client, 1050, 211, 164)
        awardItem(client, "721072", 1)

    end

end
