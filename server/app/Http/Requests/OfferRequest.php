<?php

namespace App\Http\Requests;

use Illuminate\Contracts\Validation\Validator;
use Illuminate\Foundation\Http\FormRequest;
use Illuminate\Http\Exceptions\HttpResponseException;
use Illuminate\Http\JsonResponse;
use Illuminate\Validation\ValidationException;

class OfferRequest extends FormRequest
{
    /**
     * Determine if the user is authorized to make this request.
     *
     * @return bool
     */
    public function authorize()
    {
        return true;
    }

    /**
     * Get the validation rules that apply to the request.
     *
     * @return array
     */
    public function rules()
    {
        return [
            'name' => 'required',
            'language' => 'required',
            'creator_id.*' => 'required|exists:users,user_id',
            'owner_user_id.*' => 'required|exists:users,user_id',
            'organization_id.*' => 'required|exists:organizations,id',
            'authors.*' => 'required',
        ];
    }

    public function failedValidation(Validator $validator)
    {
        $errors = (new ValidationException($validator))->errors();
        throw new HttpResponseException(response()->json(
            [
                'error' => $errors,
                'status_code' => 422,
            ],
            JsonResponse::HTTP_UNPROCESSABLE_ENTITY
        ));
    }
}
