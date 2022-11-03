import { deleteIngredientHazard } from "../../modules/ingredientHazardManager"

export const IngredientHazard = ({ hazard, hazardDelete, getIngredientHazards }) => {

    const deleteButton = () => {
        return deleteIngredientHazard(hazard.id)
            .then(() => {
                getIngredientHazards()
            })
    }
    return <>
        <li>{hazard?.hazard?.name} : {hazard?.case}  {hazardDelete ? <button className="btn" onClick={(() => deleteButton())}><i class="fa-solid fa-circle-minus"></i></button> : ""}</li>
    </>
}