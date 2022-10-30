import { deleteIngredientHazard } from "../../modules/ingredientHazardManager"
import { deleteUse } from "../../modules/useManager"

export const Use = ({ use, useDelete, getUses }) => {

    const deleteButton = () => {
        return deleteUse(use.id)
            .then(() => {
                getUses()
            })
    }
    return <>
        <li>{use.description}  {useDelete ? <button className="btn" onClick={(() => deleteButton())}><i class="fa-solid fa-circle-minus"></i></button> : ""}</li>
    </>
}